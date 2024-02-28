-- Створення таблиці "Волонтер"
CREATE TABLE Volunteer (
    id_volunteer serial PRIMARY KEY,
    name_volunteer text,
    surname_volunteer text,
    city_volunteer  text,
    email_volunteer  text,
    password_volunteer  text
);

-- Створення таблиці "Організація"
CREATE TABLE Organization (
    id_organization serial PRIMARY KEY,
    name_organization text,
    city_organization text,
    email_organization text,
    password_organization text
);

-- Створення таблиці "Донат"
CREATE TABLE Donation (
    id_donation serial PRIMARY KEY,
    category_donation text,
    name_donation text,
    type_donation text,
    sum_donation numeric(10, 2),
   object_donation text,
    number_donation NUMERIC,
    date_donation TIMESTAMP,
   status_donation text,
    photo_donation bytea
);

-- Створення таблиці "Звіт"
CREATE TABLE Report (
    id_report serial PRIMARY KEY,
    type_donation_report text,
    date_start_report TIMESTAMP,
    date_end_report TIMESTAMP,
    id_donation integer REFERENCES Donation(id_donation),
    id_organization integer REFERENCES Organization(id_organization)
);
