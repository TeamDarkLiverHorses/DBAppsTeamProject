INSERT INTO CATEGORIES (NAME, PARENT_CATEGORY_ID)
VALUES (
'Whiskey',(SELECT ID FROM CATEGORIES WHERE NAME = 'Alcohol'));

