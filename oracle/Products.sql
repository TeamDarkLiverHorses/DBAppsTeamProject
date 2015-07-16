--------------------------------------------------------
--  File created - ���������-���-16-2015   
--------------------------------------------------------
--------------------------------------------------------
--  DDL for Table CATEGORIES
--------------------------------------------------------

  CREATE TABLE "ROOT"."CATEGORIES" 
   (	"ID" NUMBER(*,0), 
	"NAME" VARCHAR2(50 BYTE), 
	"SUBCATEGORYID" NUMBER(*,0)
   ) SEGMENT CREATION IMMEDIATE 
  PCTFREE 10 PCTUSED 40 INITRANS 1 MAXTRANS 255 NOCOMPRESS LOGGING
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "SYSTEM" ;
--------------------------------------------------------
--  DDL for Table MEASURES
--------------------------------------------------------

  CREATE TABLE "ROOT"."MEASURES" 
   (	"ID" NUMBER(*,0), 
	"NAME" VARCHAR2(20 BYTE)
   ) SEGMENT CREATION IMMEDIATE 
  PCTFREE 10 PCTUSED 40 INITRANS 1 MAXTRANS 255 NOCOMPRESS LOGGING
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "SYSTEM" ;
--------------------------------------------------------
--  DDL for Table PRODUCTS
--------------------------------------------------------

  CREATE TABLE "ROOT"."PRODUCTS" 
   (	"ID" NUMBER(*,0), 
	"NAME" VARCHAR2(50 BYTE), 
	"PRICE" NUMBER(19,4), 
	"VENDORID" NUMBER(*,0), 
	"MEASUREID" NUMBER(*,0), 
	"CATEGORYID" NUMBER(*,0)
   ) SEGMENT CREATION IMMEDIATE 
  PCTFREE 10 PCTUSED 40 INITRANS 1 MAXTRANS 255 NOCOMPRESS LOGGING
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "SYSTEM" ;
--------------------------------------------------------
--  DDL for Table VENDORS
--------------------------------------------------------

  CREATE TABLE "ROOT"."VENDORS" 
   (	"ID" NUMBER(*,0), 
	"NAME" VARCHAR2(50 BYTE)
   ) SEGMENT CREATION IMMEDIATE 
  PCTFREE 10 PCTUSED 40 INITRANS 1 MAXTRANS 255 NOCOMPRESS LOGGING
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "SYSTEM" ;
REM INSERTING into ROOT.CATEGORIES
SET DEFINE OFF;
Insert into ROOT.CATEGORIES (ID,NAME,SUBCATEGORYID) values (1,'Alcohol',null);
Insert into ROOT.CATEGORIES (ID,NAME,SUBCATEGORYID) values (2,'Beer',1);
Insert into ROOT.CATEGORIES (ID,NAME,SUBCATEGORYID) values (3,'Juice',null);
Insert into ROOT.CATEGORIES (ID,NAME,SUBCATEGORYID) values (4,'Soft Drink',null);
Insert into ROOT.CATEGORIES (ID,NAME,SUBCATEGORYID) values (5,'Food',null);
Insert into ROOT.CATEGORIES (ID,NAME,SUBCATEGORYID) values (6,'Chocolate',5);
Insert into ROOT.CATEGORIES (ID,NAME,SUBCATEGORYID) values (7,'Ice Scream',5);
Insert into ROOT.CATEGORIES (ID,NAME,SUBCATEGORYID) values (8,'Snacks',5);
REM INSERTING into ROOT.MEASURES
SET DEFINE OFF;
Insert into ROOT.MEASURES (ID,NAME) values (1,'kg.');
Insert into ROOT.MEASURES (ID,NAME) values (2,'liters');
Insert into ROOT.MEASURES (ID,NAME) values (3,'pieces');
REM INSERTING into ROOT.PRODUCTS
SET DEFINE OFF;
Insert into ROOT.PRODUCTS (ID,NAME,PRICE,VENDORID,MEASUREID,CATEGORYID) values (1,'Fanta',0.9,5,2,4);
Insert into ROOT.PRODUCTS (ID,NAME,PRICE,VENDORID,MEASUREID,CATEGORYID) values (2,'Coca Cola',1.1,5,2,4);
Insert into ROOT.PRODUCTS (ID,NAME,PRICE,VENDORID,MEASUREID,CATEGORYID) values (3,'Pepsi Cola',1,4,2,4);
Insert into ROOT.PRODUCTS (ID,NAME,PRICE,VENDORID,MEASUREID,CATEGORYID) values (4,'Schweppes',0.9,6,2,4);
Insert into ROOT.PRODUCTS (ID,NAME,PRICE,VENDORID,MEASUREID,CATEGORYID) values (5,'Canada Dry',2.5,6,2,4);
Insert into ROOT.PRODUCTS (ID,NAME,PRICE,VENDORID,MEASUREID,CATEGORYID) values (6,'Hawaiian Punch',5.2,6,2,1);
Insert into ROOT.PRODUCTS (ID,NAME,PRICE,VENDORID,MEASUREID,CATEGORYID) values (7,'Nestle Chocolate',1.34,1,1,6);
Insert into ROOT.PRODUCTS (ID,NAME,PRICE,VENDORID,MEASUREID,CATEGORYID) values (8,'Chio Chips',1.25,8,1,8);
Insert into ROOT.PRODUCTS (ID,NAME,PRICE,VENDORID,MEASUREID,CATEGORYID) values (9,'Sprite',0.95,5,2,4);
Insert into ROOT.PRODUCTS (ID,NAME,PRICE,VENDORID,MEASUREID,CATEGORYID) values (10,'Mirinda',0.86,4,2,4);
Insert into ROOT.PRODUCTS (ID,NAME,PRICE,VENDORID,MEASUREID,CATEGORYID) values (11,'Sevenup',1.12,4,2,4);
Insert into ROOT.PRODUCTS (ID,NAME,PRICE,VENDORID,MEASUREID,CATEGORYID) values (12,'Almus Special',1.54,7,2,2);
Insert into ROOT.PRODUCTS (ID,NAME,PRICE,VENDORID,MEASUREID,CATEGORYID) values (13,'Gredberg',1.23,7,2,2);
Insert into ROOT.PRODUCTS (ID,NAME,PRICE,VENDORID,MEASUREID,CATEGORYID) values (14,'Almus Lager',1.34,7,2,2);
Insert into ROOT.PRODUCTS (ID,NAME,PRICE,VENDORID,MEASUREID,CATEGORYID) values (15,'Almus Dark',1.23,7,2,2);
Insert into ROOT.PRODUCTS (ID,NAME,PRICE,VENDORID,MEASUREID,CATEGORYID) values (16,'Lay Chips',1.45,9,1,8);
Insert into ROOT.PRODUCTS (ID,NAME,PRICE,VENDORID,MEASUREID,CATEGORYID) values (17,'Doritos',2.11,9,1,8);
Insert into ROOT.PRODUCTS (ID,NAME,PRICE,VENDORID,MEASUREID,CATEGORYID) values (18,'Ruffles',1.98,9,1,8);
Insert into ROOT.PRODUCTS (ID,NAME,PRICE,VENDORID,MEASUREID,CATEGORYID) values (19,'Fritos',1.98,9,1,8);
Insert into ROOT.PRODUCTS (ID,NAME,PRICE,VENDORID,MEASUREID,CATEGORYID) values (20,'Chio Tortillas',1.77,8,1,8);
Insert into ROOT.PRODUCTS (ID,NAME,PRICE,VENDORID,MEASUREID,CATEGORYID) values (21,'Chio Taccos',1.23,8,1,8);
Insert into ROOT.PRODUCTS (ID,NAME,PRICE,VENDORID,MEASUREID,CATEGORYID) values (22,'Vodka �Targovishte�',7.23,3,2,1);
Insert into ROOT.PRODUCTS (ID,NAME,PRICE,VENDORID,MEASUREID,CATEGORYID) values (23,'Beer �Zagorka�',1.56,2,2,2);
Insert into ROOT.PRODUCTS (ID,NAME,PRICE,VENDORID,MEASUREID,CATEGORYID) values (24,'Beer �Beck�s�',2.14,2,2,2);
Insert into ROOT.PRODUCTS (ID,NAME,PRICE,VENDORID,MEASUREID,CATEGORYID) values (25,'Chocolate Milka',1.34,1,1,6);
Insert into ROOT.PRODUCTS (ID,NAME,PRICE,VENDORID,MEASUREID,CATEGORYID) values (26,'Toffiffee',2.12,10,3,5);
Insert into ROOT.PRODUCTS (ID,NAME,PRICE,VENDORID,MEASUREID,CATEGORYID) values (27,'Merci (candy)',2.12,10,3,5);
Insert into ROOT.PRODUCTS (ID,NAME,PRICE,VENDORID,MEASUREID,CATEGORYID) values (28,'Nestle "Taraleji"',2.11,1,3,5);
Insert into ROOT.PRODUCTS (ID,NAME,PRICE,VENDORID,MEASUREID,CATEGORYID) values (29,'Shugar "Sladea"',0.76,11,1,5);
Insert into ROOT.PRODUCTS (ID,NAME,PRICE,VENDORID,MEASUREID,CATEGORYID) values (30,'Chaika',0.54,11,1,6);
Insert into ROOT.PRODUCTS (ID,NAME,PRICE,VENDORID,MEASUREID,CATEGORYID) values (31,'Carling Black Label',3.67,12,2,2);
Insert into ROOT.PRODUCTS (ID,NAME,PRICE,VENDORID,MEASUREID,CATEGORYID) values (32,'Carling Cider',2.67,12,2,1);
Insert into ROOT.PRODUCTS (ID,NAME,PRICE,VENDORID,MEASUREID,CATEGORYID) values (33,'Carling Black Label Big',1.54,12,2,1);
Insert into ROOT.PRODUCTS (ID,NAME,PRICE,VENDORID,MEASUREID,CATEGORYID) values (34,'Somersby cider',2.56,13,2,1);
Insert into ROOT.PRODUCTS (ID,NAME,PRICE,VENDORID,MEASUREID,CATEGORYID) values (35,'Tuborg',3.45,13,2,2);
Insert into ROOT.PRODUCTS (ID,NAME,PRICE,VENDORID,MEASUREID,CATEGORYID) values (36,'Grimbergen craft beers',2.89,13,2,2);
Insert into ROOT.PRODUCTS (ID,NAME,PRICE,VENDORID,MEASUREID,CATEGORYID) values (37,'Pirinsko',1.54,13,2,2);
Insert into ROOT.PRODUCTS (ID,NAME,PRICE,VENDORID,MEASUREID,CATEGORYID) values (38,' KITKAT',1.54,1,1,6);
Insert into ROOT.PRODUCTS (ID,NAME,PRICE,VENDORID,MEASUREID,CATEGORYID) values (39,'NESTLE LZ',1.23,1,1,6);
Insert into ROOT.PRODUCTS (ID,NAME,PRICE,VENDORID,MEASUREID,CATEGORYID) values (40,'NESTLE  GOLD FLAKES',2.45,1,1,5);
Insert into ROOT.PRODUCTS (ID,NAME,PRICE,VENDORID,MEASUREID,CATEGORYID) values (41,'NESTLE CORN FLAKES',3.73,1,1,5);
REM INSERTING into ROOT.VENDORS
SET DEFINE OFF;
Insert into ROOT.VENDORS (ID,NAME) values (1,'Nestle Sofia Corp.');
Insert into ROOT.VENDORS (ID,NAME) values (2,'Zagorka Corp.');
Insert into ROOT.VENDORS (ID,NAME) values (3,'Targovishte Bottling Company Ltd.');
Insert into ROOT.VENDORS (ID,NAME) values (4,'PepsiCo');
Insert into ROOT.VENDORS (ID,NAME) values (5,'Coca Cola');
Insert into ROOT.VENDORS (ID,NAME) values (6,'Dr Pepper Snapple Group');
Insert into ROOT.VENDORS (ID,NAME) values (7,'Lomsko Pivo AD');
Insert into ROOT.VENDORS (ID,NAME) values (10,'August Storck KG');
Insert into ROOT.VENDORS (ID,NAME) values (8,'CHIO-CHIPS KNABBERARTIKEL GMBH');
Insert into ROOT.VENDORS (ID,NAME) values (9,'Frito-Lay Inc');
Insert into ROOT.VENDORS (ID,NAME) values (11,'Zaharni Zavodi AD');
Insert into ROOT.VENDORS (ID,NAME) values (12,'Molson Coors Brewing Company');
Insert into ROOT.VENDORS (ID,NAME) values (13,'Carlsberg Group');
--------------------------------------------------------
--  Constraints for Table MEASURES
--------------------------------------------------------

  ALTER TABLE "ROOT"."MEASURES" ADD UNIQUE ("NAME")
  USING INDEX PCTFREE 10 INITRANS 2 MAXTRANS 255 COMPUTE STATISTICS 
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "SYSTEM"  ENABLE;
  ALTER TABLE "ROOT"."MEASURES" ADD PRIMARY KEY ("ID")
  USING INDEX PCTFREE 10 INITRANS 2 MAXTRANS 255 COMPUTE STATISTICS 
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "SYSTEM"  ENABLE;
  ALTER TABLE "ROOT"."MEASURES" MODIFY ("NAME" NOT NULL ENABLE);
  ALTER TABLE "ROOT"."MEASURES" MODIFY ("ID" NOT NULL ENABLE);
--------------------------------------------------------
--  Constraints for Table PRODUCTS
--------------------------------------------------------

  ALTER TABLE "ROOT"."PRODUCTS" ADD PRIMARY KEY ("ID")
  USING INDEX PCTFREE 10 INITRANS 2 MAXTRANS 255 
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "SYSTEM"  ENABLE;
  ALTER TABLE "ROOT"."PRODUCTS" MODIFY ("CATEGORYID" NOT NULL ENABLE);
  ALTER TABLE "ROOT"."PRODUCTS" MODIFY ("MEASUREID" NOT NULL ENABLE);
  ALTER TABLE "ROOT"."PRODUCTS" MODIFY ("VENDORID" NOT NULL ENABLE);
  ALTER TABLE "ROOT"."PRODUCTS" MODIFY ("PRICE" NOT NULL ENABLE);
  ALTER TABLE "ROOT"."PRODUCTS" MODIFY ("NAME" NOT NULL ENABLE);
  ALTER TABLE "ROOT"."PRODUCTS" MODIFY ("ID" NOT NULL ENABLE);
--------------------------------------------------------
--  Constraints for Table CATEGORIES
--------------------------------------------------------

  ALTER TABLE "ROOT"."CATEGORIES" ADD PRIMARY KEY ("ID")
  USING INDEX PCTFREE 10 INITRANS 2 MAXTRANS 255 
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "SYSTEM"  ENABLE;
  ALTER TABLE "ROOT"."CATEGORIES" MODIFY ("NAME" NOT NULL ENABLE);
  ALTER TABLE "ROOT"."CATEGORIES" MODIFY ("ID" NOT NULL ENABLE);
--------------------------------------------------------
--  Constraints for Table VENDORS
--------------------------------------------------------

  ALTER TABLE "ROOT"."VENDORS" ADD UNIQUE ("NAME")
  USING INDEX PCTFREE 10 INITRANS 2 MAXTRANS 255 COMPUTE STATISTICS 
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "SYSTEM"  ENABLE;
  ALTER TABLE "ROOT"."VENDORS" ADD PRIMARY KEY ("ID")
  USING INDEX PCTFREE 10 INITRANS 2 MAXTRANS 255 COMPUTE STATISTICS 
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "SYSTEM"  ENABLE;
  ALTER TABLE "ROOT"."VENDORS" MODIFY ("NAME" NOT NULL ENABLE);
  ALTER TABLE "ROOT"."VENDORS" MODIFY ("ID" NOT NULL ENABLE);
--------------------------------------------------------
--  Ref Constraints for Table CATEGORIES
--------------------------------------------------------

  ALTER TABLE "ROOT"."CATEGORIES" ADD CONSTRAINT "FK_CHILD" FOREIGN KEY ("SUBCATEGORYID")
	  REFERENCES "ROOT"."CATEGORIES" ("ID") ENABLE;
--------------------------------------------------------
--  Ref Constraints for Table PRODUCTS
--------------------------------------------------------

  ALTER TABLE "ROOT"."PRODUCTS" ADD CONSTRAINT "FK_CATEGORIES" FOREIGN KEY ("CATEGORYID")
	  REFERENCES "ROOT"."CATEGORIES" ("ID") ENABLE;
  ALTER TABLE "ROOT"."PRODUCTS" ADD CONSTRAINT "FK_MEASURES" FOREIGN KEY ("MEASUREID")
	  REFERENCES "ROOT"."MEASURES" ("ID") ENABLE;
  ALTER TABLE "ROOT"."PRODUCTS" ADD CONSTRAINT "FK_VENDORS" FOREIGN KEY ("VENDORID")
	  REFERENCES "ROOT"."VENDORS" ("ID") ENABLE;