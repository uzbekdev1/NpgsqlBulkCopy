// The following ifdef block is the standard way of creating macros which make exporting 
// from a DLL simpler. All files within this DLL are compiled with the PGCPPDLL8X_EXPORTS
// symbol defined on the command line. This symbol should not be defined on any project
// that uses this DLL. This way any other project whose source files include this file see 
// PGCPPDLL8X_API functions as being imported from a DLL, whereas this DLL sees symbols
// defined with this macro as being exported.
#ifdef PGCPPDLL8X_EXPORTS
#define PGCPPDLL8X_API __declspec(dllexport)
#else
#define PGCPPDLL8X_API __declspec(dllimport)
#endif

/*
references
*/
#include <string> 
#include <iostream>
#include "libpq-fe.h"
#include <vector>  
#include <windows.h> 

/*
namespaces
*/
using namespace std;

/*
templates
*/
template <class Function>
__int64 call_elapsed_time(Function&& f)
{
	__int64 begin = GetTickCount();

	f();

	__int64 end = GetTickCount();

	return  end - begin;
}

/*
options
*/
const char* ENCODING = "UTF8";
const char* DEFAULT_HOST_ADDR = "127.0.0.1";
const int DEFAULT_PORT = 5432;

/*
global fields
*/
extern PGCPPDLL8X_API PGconn* conn_;
extern PGCPPDLL8X_API char* colums_;
extern PGCPPDLL8X_API vector<char*>* rows_;
extern PGCPPDLL8X_API char* tableName_;
extern PGCPPDLL8X_API int batchSize_;

/*
Access to native API
*/
#ifdef __cplusplus
extern "C"
{
#endif 

	/*
	tools
	*/
	PGCPPDLL8X_API void openLocaleConn(const char* dbname, const char* user, const char* password);
	PGCPPDLL8X_API void openRemoteConn(const char* hostaddr, const int port, const char* dbname, const char* user, const char* password);
	PGCPPDLL8X_API void closeConn(void);
	PGCPPDLL8X_API void cleanUp(void);

	/*
	settings
	*/
	PGCPPDLL8X_API void setColumns(const char* columns);
	PGCPPDLL8X_API void setTableName(const char* tableName);
	PGCPPDLL8X_API void setBatchSize(const int batchSize);

	/*
	core
	*/
	PGCPPDLL8X_API void addRow(const char* row);
	PGCPPDLL8X_API void runBulkCopy();

	/*
	info
	*/
	PGCPPDLL8X_API const	bool hasConnStatus(void);
	PGCPPDLL8X_API const int libVersion(void);

#ifdef __cplusplus
}
#endif 