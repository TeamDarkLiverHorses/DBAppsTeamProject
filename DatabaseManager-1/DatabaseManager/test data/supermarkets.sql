--------------------------------------------------------
--  File created - Friday-July-17-2015   
--------------------------------------------------------
--------------------------------------------------------
--  DDL for Sequence CATEGORIES_SEQ
--------------------------------------------------------

   CREATE SEQUENCE  "SUPERMARKETS"."CATEGORIES_SEQ"  MINVALUE 1 MAXVALUE 9999999999999999999999999999 INCREMENT BY 1 START WITH 21 CACHE 20 NOORDER  NOCYCLE ;
--------------------------------------------------------
--  DDL for Sequence MEASURES_SEQ
--------------------------------------------------------

   CREATE SEQUENCE  "SUPERMARKETS"."MEASURES_SEQ"  MINVALUE 1 MAXVALUE 9999999999999999999999999999 INCREMENT BY 1 START WITH 21 CACHE 20 NOORDER  NOCYCLE ;
--------------------------------------------------------
--  DDL for Sequence PRODUCTS_SEQ
--------------------------------------------------------

   CREATE SEQUENCE  "SUPERMARKETS"."PRODUCTS_SEQ"  MINVALUE 1 MAXVALUE 9999999999999999999999999999 INCREMENT BY 1 START WITH 61 CACHE 20 NOORDER  NOCYCLE ;
--------------------------------------------------------
--  DDL for Sequence VENDORS_SEQ
--------------------------------------------------------

   CREATE SEQUENCE  "SUPERMARKETS"."VENDORS_SEQ"  MINVALUE 1 MAXVALUE 9999999999999999999999999999 INCREMENT BY 1 START WITH 21 CACHE 20 NOORDER  NOCYCLE ;
--------------------------------------------------------
--  DDL for Table CATEGORIES
--------------------------------------------------------

  CREATE TABLE "SUPERMARKETS"."CATEGORIES" 
   (	"ID" NUMBER(*,0), 
	"NAME" NVARCHAR2(100), 
	"PARENT_CATEGORY_ID" NUMBER(*,0)
   ) SEGMENT CREATION IMMEDIATE 
  PCTFREE 10 PCTUSED 40 INITRANS 1 MAXTRANS 255 NOCOMPRESS LOGGING
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "SYSTEM" ;
--------------------------------------------------------
--  DDL for Table MEASURES
--------------------------------------------------------

  CREATE TABLE "SUPERMARKETS"."MEASURES" 
   (	"ID" NUMBER(*,0), 
	"NAME" NVARCHAR2(20)
   ) SEGMENT CREATION IMMEDIATE 
  PCTFREE 10 PCTUSED 40 INITRANS 1 MAXTRANS 255 NOCOMPRESS LOGGING
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "SYSTEM" ;
--------------------------------------------------------
--  DDL for Table PRODUCTS
--------------------------------------------------------

  CREATE TABLE "SUPERMARKETS"."PRODUCTS" 
   (	"ID" NUMBER(*,0), 
	"NAME" NVARCHAR2(100), 
	"PRICE" NUMBER(19,4), 
	"VENDOR_ID" NUMBER(*,0), 
	"MEASURE_ID" NUMBER(*,0), 
	"CATEGORY_ID" NUMBER(*,0)
   ) SEGMENT CREATION IMMEDIATE 
  PCTFREE 10 PCTUSED 40 INITRANS 1 MAXTRANS 255 NOCOMPRESS LOGGING
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "SYSTEM" ;
--------------------------------------------------------
--  DDL for Table VENDORS
--------------------------------------------------------

  CREATE TABLE "SUPERMARKETS"."VENDORS" 
   (	"ID" NUMBER(*,0), 
	"NAME" NVARCHAR2(100)
   ) SEGMENT CREATION IMMEDIATE 
  PCTFREE 10 PCTUSED 40 INITRANS 1 MAXTRANS 255 NOCOMPRESS LOGGING
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "SYSTEM" ;
REM INSERTING into SUPERMARKETS.CATEGORIES
SET DEFINE OFF;
Insert into SUPERMARKETS.CATEGORIES (ID,NAME,PARENT_CATEGORY_ID) values (1,'Alcohol',null);
Insert into SUPERMARKETS.CATEGORIES (ID,NAME,PARENT_CATEGORY_ID) values (2,'Beer',1);
Insert into SUPERMARKETS.CATEGORIES (ID,NAME,PARENT_CATEGORY_ID) values (3,'Juice',null);
Insert into SUPERMARKETS.CATEGORIES (ID,NAME,PARENT_CATEGORY_ID) values (4,'Soft Drink',null);
Insert into SUPERMARKETS.CATEGORIES (ID,NAME,PARENT_CATEGORY_ID) values (5,'Food',null);
Insert into SUPERMARKETS.CATEGORIES (ID,NAME,PARENT_CATEGORY_ID) values (6,'Chocolate',5);
Insert into SUPERMARKETS.CATEGORIES (ID,NAME,PARENT_CATEGORY_ID) values (7,'Ice Scream',5);
Insert into SUPERMARKETS.CATEGORIES (ID,NAME,PARENT_CATEGORY_ID) values (8,'Snacks',5);
Insert into SUPERMARKETS.CATEGORIES (ID,NAME,PARENT_CATEGORY_ID) values (9,'Juice',null);
Insert into SUPERMARKETS.CATEGORIES (ID,NAME,PARENT_CATEGORY_ID) values (10,'Soft Drink',null);
Insert into SUPERMARKETS.CATEGORIES (ID,NAME,PARENT_CATEGORY_ID) values (11,'Food',null);
Insert into SUPERMARKETS.CATEGORIES (ID,NAME,PARENT_CATEGORY_ID) values (12,'Chocolate',5);
Insert into SUPERMARKETS.CATEGORIES (ID,NAME,PARENT_CATEGORY_ID) values (13,'Ice Scream',5);
Insert into SUPERMARKETS.CATEGORIES (ID,NAME,PARENT_CATEGORY_ID) values (14,'Snacks',5);
Insert into SUPERMARKETS.CATEGORIES (ID,NAME,PARENT_CATEGORY_ID) values (15,'Bread',5);
Insert into SUPERMARKETS.CATEGORIES (ID,NAME,PARENT_CATEGORY_ID) values (16,'Vodka',1);
Insert into SUPERMARKETS.CATEGORIES (ID,NAME,PARENT_CATEGORY_ID) values (17,'Sugar',5);
REM INSERTING into SUPERMARKETS.MEASURES
SET DEFINE OFF;
Insert into SUPERMARKETS.MEASURES (ID,NAME) values (1,'kg.');
Insert into SUPERMARKETS.MEASURES (ID,NAME) values (2,'liters');
Insert into SUPERMARKETS.MEASURES (ID,NAME) values (3,'pieces');
REM INSERTING into SUPERMARKETS.PRODUCTS
SET DEFINE OFF;
Insert into SUPERMARKETS.PRODUCTS (ID,NAME,PRICE,VENDOR_ID,MEASURE_ID,CATEGORY_ID) values (1,'Fanta',0.9,5,2,4);
Insert into SUPERMARKETS.PRODUCTS (ID,NAME,PRICE,VENDOR_ID,MEASURE_ID,CATEGORY_ID) values (2,'Coca Cola',1.1,5,2,4);
Insert into SUPERMARKETS.PRODUCTS (ID,NAME,PRICE,VENDOR_ID,MEASURE_ID,CATEGORY_ID) values (3,'Pepsi Cola',1,4,2,4);
Insert into SUPERMARKETS.PRODUCTS (ID,NAME,PRICE,VENDOR_ID,MEASURE_ID,CATEGORY_ID) values (4,'Schweppes',0.9,6,2,4);
Insert into SUPERMARKETS.PRODUCTS (ID,NAME,PRICE,VENDOR_ID,MEASURE_ID,CATEGORY_ID) values (5,'Canada Dry',2.5,6,2,4);
Insert into SUPERMARKETS.PRODUCTS (ID,NAME,PRICE,VENDOR_ID,MEASURE_ID,CATEGORY_ID) values (6,'Hawaiian Punch',5.2,6,2,1);
Insert into SUPERMARKETS.PRODUCTS (ID,NAME,PRICE,VENDOR_ID,MEASURE_ID,CATEGORY_ID) values (7,'Boss Caramel',1.34,1,1,6);
Insert into SUPERMARKETS.PRODUCTS (ID,NAME,PRICE,VENDOR_ID,MEASURE_ID,CATEGORY_ID) values (8,'Chio Chips',1.25,8,1,8);
Insert into SUPERMARKETS.PRODUCTS (ID,NAME,PRICE,VENDOR_ID,MEASURE_ID,CATEGORY_ID) values (9,'Sprite',0.95,5,2,4);
Insert into SUPERMARKETS.PRODUCTS (ID,NAME,PRICE,VENDOR_ID,MEASURE_ID,CATEGORY_ID) values (10,'Mirinda',0.86,4,2,4);
Insert into SUPERMARKETS.PRODUCTS (ID,NAME,PRICE,VENDOR_ID,MEASURE_ID,CATEGORY_ID) values (11,'Sevenup',1.12,4,2,4);
Insert into SUPERMARKETS.PRODUCTS (ID,NAME,PRICE,VENDOR_ID,MEASURE_ID,CATEGORY_ID) values (12,'Almus Special',1.54,7,2,2);
Insert into SUPERMARKETS.PRODUCTS (ID,NAME,PRICE,VENDOR_ID,MEASURE_ID,CATEGORY_ID) values (13,'Gredberg',1.23,7,2,2);
Insert into SUPERMARKETS.PRODUCTS (ID,NAME,PRICE,VENDOR_ID,MEASURE_ID,CATEGORY_ID) values (14,'Almus Lager',1.34,7,2,2);
Insert into SUPERMARKETS.PRODUCTS (ID,NAME,PRICE,VENDOR_ID,MEASURE_ID,CATEGORY_ID) values (15,'Almus Dark',1.23,7,2,2);
Insert into SUPERMARKETS.PRODUCTS (ID,NAME,PRICE,VENDOR_ID,MEASURE_ID,CATEGORY_ID) values (16,'Lay Chips',1.45,9,1,8);
Insert into SUPERMARKETS.PRODUCTS (ID,NAME,PRICE,VENDOR_ID,MEASURE_ID,CATEGORY_ID) values (17,'Doritos',2.11,9,1,8);
Insert into SUPERMARKETS.PRODUCTS (ID,NAME,PRICE,VENDOR_ID,MEASURE_ID,CATEGORY_ID) values (18,'Ruffles',1.98,9,1,8);
Insert into SUPERMARKETS.PRODUCTS (ID,NAME,PRICE,VENDOR_ID,MEASURE_ID,CATEGORY_ID) values (19,'Fritos',1.98,9,1,8);
Insert into SUPERMARKETS.PRODUCTS (ID,NAME,PRICE,VENDOR_ID,MEASURE_ID,CATEGORY_ID) values (20,'Chio Tortillas',1.77,8,1,8);
Insert into SUPERMARKETS.PRODUCTS (ID,NAME,PRICE,VENDOR_ID,MEASURE_ID,CATEGORY_ID) values (21,'Chio Taccos',1.23,8,1,8);
Insert into SUPERMARKETS.PRODUCTS (ID,NAME,PRICE,VENDOR_ID,MEASURE_ID,CATEGORY_ID) values (22,'Targovishte 0,7L',7.23,3,2,1);
Insert into SUPERMARKETS.PRODUCTS (ID,NAME,PRICE,VENDOR_ID,MEASURE_ID,CATEGORY_ID) values (23,'Zagorka 0,5L Bottle',1.56,2,2,2);
Insert into SUPERMARKETS.PRODUCTS (ID,NAME,PRICE,VENDOR_ID,MEASURE_ID,CATEGORY_ID) values (24,'Beck�s 0,5L Bottle',2.14,2,2,2);
Insert into SUPERMARKETS.PRODUCTS (ID,NAME,PRICE,VENDOR_ID,MEASURE_ID,CATEGORY_ID) values (25,'Milka Raisins',1.34,1,1,6);
Insert into SUPERMARKETS.PRODUCTS (ID,NAME,PRICE,VENDOR_ID,MEASURE_ID,CATEGORY_ID) values (26,'Toffiffee',2.12,10,3,5);
Insert into SUPERMARKETS.PRODUCTS (ID,NAME,PRICE,VENDOR_ID,MEASURE_ID,CATEGORY_ID) values (27,'Merci (candy)',2.12,10,3,5);
Insert into SUPERMARKETS.PRODUCTS (ID,NAME,PRICE,VENDOR_ID,MEASURE_ID,CATEGORY_ID) values (28,'Taraleji',2.11,1,3,5);
Insert into SUPERMARKETS.PRODUCTS (ID,NAME,PRICE,VENDOR_ID,MEASURE_ID,CATEGORY_ID) values (29,'Sladea',0.76,11,1,17);
Insert into SUPERMARKETS.PRODUCTS (ID,NAME,PRICE,VENDOR_ID,MEASURE_ID,CATEGORY_ID) values (30,'Chaika',0.54,11,1,6);
Insert into SUPERMARKETS.PRODUCTS (ID,NAME,PRICE,VENDOR_ID,MEASURE_ID,CATEGORY_ID) values (31,'Carling Black Label',3.67,12,2,2);
Insert into SUPERMARKETS.PRODUCTS (ID,NAME,PRICE,VENDOR_ID,MEASURE_ID,CATEGORY_ID) values (32,'Carling Cider',2.67,12,2,1);
Insert into SUPERMARKETS.PRODUCTS (ID,NAME,PRICE,VENDOR_ID,MEASURE_ID,CATEGORY_ID) values (33,'Carling Black Label Big',1.54,12,2,1);
Insert into SUPERMARKETS.PRODUCTS (ID,NAME,PRICE,VENDOR_ID,MEASURE_ID,CATEGORY_ID) values (34,'Somersby cider',2.56,13,2,1);
Insert into SUPERMARKETS.PRODUCTS (ID,NAME,PRICE,VENDOR_ID,MEASURE_ID,CATEGORY_ID) values (35,'Tuborg',3.45,13,2,2);
Insert into SUPERMARKETS.PRODUCTS (ID,NAME,PRICE,VENDOR_ID,MEASURE_ID,CATEGORY_ID) values (36,'Grimbergen craft beers',2.89,13,2,2);
Insert into SUPERMARKETS.PRODUCTS (ID,NAME,PRICE,VENDOR_ID,MEASURE_ID,CATEGORY_ID) values (37,'Pirinsko 0,5L Bottle',1.54,13,2,2);
Insert into SUPERMARKETS.PRODUCTS (ID,NAME,PRICE,VENDOR_ID,MEASURE_ID,CATEGORY_ID) values (38,'KITKAT',1.54,1,1,6);
Insert into SUPERMARKETS.PRODUCTS (ID,NAME,PRICE,VENDOR_ID,MEASURE_ID,CATEGORY_ID) values (39,'LZ',1.23,1,1,6);
Insert into SUPERMARKETS.PRODUCTS (ID,NAME,PRICE,VENDOR_ID,MEASURE_ID,CATEGORY_ID) values (40,'GOLD FLAKES',2.45,1,1,5);
Insert into SUPERMARKETS.PRODUCTS (ID,NAME,PRICE,VENDOR_ID,MEASURE_ID,CATEGORY_ID) values (41,'CORN FLAKES',3.73,1,1,5);
REM INSERTING into SUPERMARKETS.VENDORS
SET DEFINE OFF;
Insert into SUPERMARKETS.VENDORS (ID,NAME) values (1,'Nestle Sofia Corp.');
Insert into SUPERMARKETS.VENDORS (ID,NAME) values (2,'Zagorka Corp.');
Insert into SUPERMARKETS.VENDORS (ID,NAME) values (3,'Targovishte Bottling Company Ltd.');
Insert into SUPERMARKETS.VENDORS (ID,NAME) values (4,'PepsiCo');
Insert into SUPERMARKETS.VENDORS (ID,NAME) values (5,'Coca Cola');
Insert into SUPERMARKETS.VENDORS (ID,NAME) values (6,'Dr Pepper Snapple Group');
Insert into SUPERMARKETS.VENDORS (ID,NAME) values (7,'Lomsko Pivo AD');
Insert into SUPERMARKETS.VENDORS (ID,NAME) values (8,'CHIO-CHIPS KNABBERARTIKEL GMBH');
Insert into SUPERMARKETS.VENDORS (ID,NAME) values (9,'Frito-Lay Inc');
Insert into SUPERMARKETS.VENDORS (ID,NAME) values (10,'Zaharni Zavodi AD');
Insert into SUPERMARKETS.VENDORS (ID,NAME) values (11,'Molson Coors Brewing Company');
Insert into SUPERMARKETS.VENDORS (ID,NAME) values (12,'Carlsberg Group');
--------------------------------------------------------
--  DDL for Index CATEGORIES_PK
--------------------------------------------------------

  CREATE UNIQUE INDEX "SUPERMARKETS"."CATEGORIES_PK" ON "SUPERMARKETS"."CATEGORIES" ("ID") 
  PCTFREE 10 INITRANS 2 MAXTRANS 255 
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "SYSTEM" ;
--------------------------------------------------------
--  DDL for Index MEASURES_PK
--------------------------------------------------------

  CREATE UNIQUE INDEX "SUPERMARKETS"."MEASURES_PK" ON "SUPERMARKETS"."MEASURES" ("ID") 
  PCTFREE 10 INITRANS 2 MAXTRANS 255 
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "SYSTEM" ;
--------------------------------------------------------
--  DDL for Index PRODUCTS_PK
--------------------------------------------------------

  CREATE UNIQUE INDEX "SUPERMARKETS"."PRODUCTS_PK" ON "SUPERMARKETS"."PRODUCTS" ("ID") 
  PCTFREE 10 INITRANS 2 MAXTRANS 255 
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "SYSTEM" ;
--------------------------------------------------------
--  DDL for Index VENDORS_PK
--------------------------------------------------------

  CREATE UNIQUE INDEX "SUPERMARKETS"."VENDORS_PK" ON "SUPERMARKETS"."VENDORS" ("ID") 
  PCTFREE 10 INITRANS 2 MAXTRANS 255 
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "SYSTEM" ;
--------------------------------------------------------
--  Constraints for Table MEASURES
--------------------------------------------------------

  ALTER TABLE "SUPERMARKETS"."MEASURES" ADD CONSTRAINT "MEASURES_PK" PRIMARY KEY ("ID")
  USING INDEX PCTFREE 10 INITRANS 2 MAXTRANS 255 
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "SYSTEM"  ENABLE;
  ALTER TABLE "SUPERMARKETS"."MEASURES" MODIFY ("NAME" NOT NULL ENABLE);
  ALTER TABLE "SUPERMARKETS"."MEASURES" MODIFY ("ID" NOT NULL ENABLE);
--------------------------------------------------------
--  Constraints for Table CATEGORIES
--------------------------------------------------------

  ALTER TABLE "SUPERMARKETS"."CATEGORIES" ADD CONSTRAINT "CATEGORIES_PK" PRIMARY KEY ("ID")
  USING INDEX PCTFREE 10 INITRANS 2 MAXTRANS 255 
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "SYSTEM"  ENABLE;
  ALTER TABLE "SUPERMARKETS"."CATEGORIES" MODIFY ("NAME" NOT NULL ENABLE);
  ALTER TABLE "SUPERMARKETS"."CATEGORIES" MODIFY ("ID" NOT NULL ENABLE);
--------------------------------------------------------
--  Constraints for Table VENDORS
--------------------------------------------------------

  ALTER TABLE "SUPERMARKETS"."VENDORS" ADD CONSTRAINT "VENDORS_PK" PRIMARY KEY ("ID")
  USING INDEX PCTFREE 10 INITRANS 2 MAXTRANS 255 
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "SYSTEM"  ENABLE;
  ALTER TABLE "SUPERMARKETS"."VENDORS" MODIFY ("NAME" NOT NULL ENABLE);
  ALTER TABLE "SUPERMARKETS"."VENDORS" MODIFY ("ID" NOT NULL ENABLE);
--------------------------------------------------------
--  Constraints for Table PRODUCTS
--------------------------------------------------------

  ALTER TABLE "SUPERMARKETS"."PRODUCTS" ADD CONSTRAINT "PRODUCTS_PK" PRIMARY KEY ("ID")
  USING INDEX PCTFREE 10 INITRANS 2 MAXTRANS 255 
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "SYSTEM"  ENABLE;
  ALTER TABLE "SUPERMARKETS"."PRODUCTS" MODIFY ("CATEGORY_ID" NOT NULL ENABLE);
  ALTER TABLE "SUPERMARKETS"."PRODUCTS" MODIFY ("MEASURE_ID" NOT NULL ENABLE);
  ALTER TABLE "SUPERMARKETS"."PRODUCTS" MODIFY ("VENDOR_ID" NOT NULL ENABLE);
  ALTER TABLE "SUPERMARKETS"."PRODUCTS" MODIFY ("PRICE" NOT NULL ENABLE);
  ALTER TABLE "SUPERMARKETS"."PRODUCTS" MODIFY ("NAME" NOT NULL ENABLE);
  ALTER TABLE "SUPERMARKETS"."PRODUCTS" MODIFY ("ID" NOT NULL ENABLE);
--------------------------------------------------------
--  DDL for Trigger CATEGORIES_TRG
--------------------------------------------------------

  CREATE OR REPLACE TRIGGER "SUPERMARKETS"."CATEGORIES_TRG" 
BEFORE INSERT ON CATEGORIES 
FOR EACH ROW 
BEGIN
  <<COLUMN_SEQUENCES>>
  BEGIN
    IF INSERTING AND :NEW.ID IS NULL THEN
      SELECT CATEGORIES_SEQ.NEXTVAL INTO :NEW.ID FROM SYS.DUAL;
    END IF;
  END COLUMN_SEQUENCES;
END;
/
ALTER TRIGGER "SUPERMARKETS"."CATEGORIES_TRG" ENABLE;
--------------------------------------------------------
--  DDL for Trigger MEASURES_TRG
--------------------------------------------------------

  CREATE OR REPLACE TRIGGER "SUPERMARKETS"."MEASURES_TRG" 
BEFORE INSERT ON MEASURES 
FOR EACH ROW 
BEGIN
  <<COLUMN_SEQUENCES>>
  BEGIN
    IF INSERTING AND :NEW.ID IS NULL THEN
      SELECT MEASURES_SEQ.NEXTVAL INTO :NEW.ID FROM SYS.DUAL;
    END IF;
  END COLUMN_SEQUENCES;
END;
/
ALTER TRIGGER "SUPERMARKETS"."MEASURES_TRG" ENABLE;
--------------------------------------------------------
--  DDL for Trigger PRODUCTS_TRG
--------------------------------------------------------

  CREATE OR REPLACE TRIGGER "SUPERMARKETS"."PRODUCTS_TRG" 
BEFORE INSERT ON PRODUCTS 
FOR EACH ROW 
BEGIN
  <<COLUMN_SEQUENCES>>
  BEGIN
    IF INSERTING AND :NEW.ID IS NULL THEN
      SELECT PRODUCTS_SEQ.NEXTVAL INTO :NEW.ID FROM SYS.DUAL;
    END IF;
  END COLUMN_SEQUENCES;
END;
/
ALTER TRIGGER "SUPERMARKETS"."PRODUCTS_TRG" ENABLE;
--------------------------------------------------------
--  DDL for Trigger VENDORS_TRG
--------------------------------------------------------

  CREATE OR REPLACE TRIGGER "SUPERMARKETS"."VENDORS_TRG" 
BEFORE INSERT ON VENDORS 
FOR EACH ROW 
BEGIN
  <<COLUMN_SEQUENCES>>
  BEGIN
    IF INSERTING AND :NEW.ID IS NULL THEN
      SELECT VENDORS_SEQ.NEXTVAL INTO :NEW.ID FROM SYS.DUAL;
    END IF;
  END COLUMN_SEQUENCES;
END;
/
ALTER TRIGGER "SUPERMARKETS"."VENDORS_TRG" ENABLE;