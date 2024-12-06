CREATE TABLE public."Dealers" (
    "Id"      SERIAL     NOT NULL,
    "Name"    TEXT       NOT NULL,
    "City"    TEXT       NOT NULL,
    "Address" TEXT       NOT NULL,
    "Area"    TEXT       NOT NULL,
    "Rating"  REAL       NOT NULL,
    PRIMARY KEY ("Id")
);
