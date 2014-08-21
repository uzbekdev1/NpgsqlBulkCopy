// PgCPP.DLL.8.x.cpp : Defines the exported functions for the DLL application.
//


/*
references
*/
#include "stdafx.h"
#include "PgCPP.DLL.8.x.h"   
#include <thread>      
#include <math.h>

/*
namespaces
*/
using namespace this_thread;

/*
global variables
*/
PGCPPDLL8X_API char* colums_ = nullptr;
PGCPPDLL8X_API PGconn* conn_ = nullptr;
PGCPPDLL8X_API vector<char*>* rows_ = nullptr;
PGCPPDLL8X_API char* tableName_ = nullptr;
PGCPPDLL8X_API int batchSize_ = 0;

/*
inline members
*/

void printLastError(){
	cout << "Error message : " << PQerrorMessage(conn_) << endl;
}

void  buildBulkCopy(const vector<char*>* rows){

	string query = string("INSERT INTO ") + string(tableName_) +
		string("(") + string(colums_) + string(")") +
		string(" VALUES");
	const size_t rowSize = rows->size();

	for (size_t i = 0; i < rowSize; i++){
		if (i > 0){
			query.append(",");
		}

		query.append(string("(") + string(rows->at(i)) + string(")"));
	}

	const char* sql = query.c_str();

	PGresult* result = PQexec(conn_, sql);

	if (PQresultStatus(result) != PGRES_COMMAND_OK){

		printLastError();

		throw exception("Result to failed");
	}

	PQclear(result);

	cout << "Copy to " << rowSize << " rows" << endl;
}

vector<char*>* getPartitionRows(const int partIndex){
	const int totalSize = rows_->size();
	const int start = partIndex*batchSize_;
	int stop = partIndex*batchSize_ + batchSize_;
	vector<char*>* rows = new vector<char*>();

	if (stop > totalSize - 1){
		stop = totalSize;
	}

	rows->assign(rows_->begin() + start, rows_->begin() + stop);

	return rows;
}

/*
external members
*/
PGCPPDLL8X_API void  openLocaleConn(const char* dbname, const char* user, const char* password){
	openRemoteConn(DEFAULT_HOST_ADDR, DEFAULT_PORT, dbname, user, password);
}

PGCPPDLL8X_API void  openRemoteConn(const char* hostaddr, const int port, const char* dbname, const char* user, const char* password){
	const string connStrAppend = string("hostaddr = ") + string(hostaddr) +
		string(" port = ") + to_string(port) +
		string(" dbname = ") + string(dbname) +
		string(" user = ") + string(user) +
		string(" password = ") + string(password);
	const char* connStr = connStrAppend.c_str();

	conn_ = PQconnectdb(connStr);

	cout << "Connection info : " << connStr << endl;

	if (!hasConnStatus()){

		printLastError();

		throw exception("Connection to database failed");
	}

	PQsetClientEncoding(conn_, ENCODING);

	cout << "Connection to database succeeded" << endl;
}

PGCPPDLL8X_API const bool  hasConnStatus(void){
	return	conn_ != nullptr &&
		PQstatus(conn_) == CONNECTION_OK;
}

PGCPPDLL8X_API const int libVersion(void){
	const int version = PQserverVersion(conn_);

	return version;
}

PGCPPDLL8X_API void setColumns(const char* columns){

	if (columns == nullptr)
		throw exception("Columns is null");

	char* data = const_cast<char*>(columns);

	colums_ = data;
}

PGCPPDLL8X_API void addRow(const char* row){

	if (row == nullptr)
		throw exception("Row is null");

	if (rows_ == nullptr)
		rows_ = new vector<char*>();

	char* data = const_cast<char*>(row);

	rows_->push_back(data);
}

PGCPPDLL8X_API void setTableName(const char* tableName){
	if (tableName == nullptr)
		throw exception("Table name is null");

	cout << "Table name : " << tableName << endl;

	tableName_ = const_cast<char*>(tableName);
}

PGCPPDLL8X_API void setBatchSize(const int batchSize){
	if (batchSize == 0)
		throw exception("Batch size is 0");

	cout << "Batch size : " << batchSize << endl;

	batchSize_ = batchSize;
}

PGCPPDLL8X_API void runBulkCopy(){

	const __int64 elapsed = call_elapsed_time([&] {

		const size_t totalSize = rows_->size();
		const size_t partSize = static_cast<size_t>(ceil(static_cast<double>(totalSize) / batchSize_));

		/*
		vector<thread*>* thrs = new vector<thread*>();
		for (size_t partIndex = 0; partIndex < partSize; partIndex++){
		const vector<char*>* rows = getPartitionRows(partIndex);
		thread* thr = new thread(buildBulkCopy, rows);

		thrs->push_back(thr);
		}

		yield();

		for (std::vector<thread*>::iterator thr = thrs->begin(); thr != thrs->end(); ++thr)
		(*thr)->join();
		*/

		for (size_t partIndex = 0; partIndex < partSize; partIndex++){
			const vector<char*>* rows = getPartitionRows(partIndex);

			buildBulkCopy(rows);
		}

	});

	cout << "Elapted time : " << elapsed << " ms" << endl;
}

PGCPPDLL8X_API void closeConn(void){
	if (!hasConnStatus())
		throw exception("Database connection closing to failure");

	PQfinish(conn_);

	cout << "Database connection closing to success" << endl;
}

PGCPPDLL8X_API void cleanUp(void){

	if (conn_ != nullptr){

		if (hasConnStatus())
			closeConn();

		PQflush(conn_);

		conn_ = nullptr;
	}

	if (colums_ != nullptr)
		colums_ = nullptr;

	if (tableName_ != nullptr)
		tableName_ = nullptr;

	if (rows_ != nullptr){

		if (!rows_->empty())
			rows_->shrink_to_fit();

		rows_ = nullptr;
	}

	if (batchSize_ != 0)
		batchSize_ = 0;

	cout << "Storage cleaned" << endl;
}

