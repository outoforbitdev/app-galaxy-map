#! /bin/bash

POSTGRES_COMMAND="psql -U postgres -d galaxy -c"

copy_from_csv() {
    DATA_FILE=$1
    TABLE_NAME=$2
    PGPASSWORD=password $POSTGRES_COMMAND "COPY $TABLE_NAME ($(head -1 $DATA_FILE))
        FROM '$DATA_FILE' DELIMITER ',' HEADER;"
}

PGPASSWORD=password $POSTGRES_COMMAND "TRUNCATE instances CASCADE;"
PGPASSWORD=password $POSTGRES_COMMAND "COPY instances FROM '/data/db/instances.csv' DELIMITER ',' HEADER;"

copy_from_csv /data/db/solar-systems.csv solar_systems
copy_from_csv /data/db/spacelanes.csv spacelanes
copy_from_csv /data/db/planets.csv planets
copy_from_csv /data/db/governments.csv governments
copy_from_csv /data/db/planet-governments.csv planet_governments
copy_from_csv /data/db/government-governments.csv government_governments
