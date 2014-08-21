-- Database: northwind

-- DROP DATABASE northwind;

CREATE DATABASE northwind
  WITH OWNER = postgres
       ENCODING = 'UTF8'
       TABLESPACE = pg_default
       LC_COLLATE = 'English_United States.1252'
       LC_CTYPE = 'English_United States.1252'
       CONNECTION LIMIT = -1;
	   
-- Table: employees

-- DROP TABLE employees;

CREATE TABLE employees
(
  "EmployeeID" smallint NOT NULL,
  "LastName" character varying(20) NOT NULL,
  "FirstName" character varying(10) NOT NULL,
  "Title" character varying(30),
  "TitleOfCourtesy" character varying(25),
  "BirthDate" date,
  "HireDate" date,
  "Address" character varying(60),
  "City" character varying(15),
  "Region" character varying(15),
  "PostalCode" character varying(10),
  "Country" character varying(15),
  "HomePhone" character varying(24),
  "Extension" character varying(4),
  "Photo" bytea,
  "Notes" text,
  "ReportsTo" smallint,
  "PhotoPath" character varying(255),
  CONSTRAINT pk_employees PRIMARY KEY ("EmployeeID")
)
WITH (
  OIDS=FALSE
);
ALTER TABLE employees
  OWNER TO postgres;
