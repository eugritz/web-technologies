CREATE TABLE public."Cars" (
    "Id"    SERIAL     NOT NULL,
    "Firm"  TEXT       NOT NULL,
    "Model" TEXT       NOT NULL,
    "Year"  INTEGER    NOT NULL,
    "Power" INTEGER    NOT NULL,
    "Color" TEXT       NOT NULL,
    "Price" REAL       NOT NULL,
    PRIMARY KEY ("Id")
);
