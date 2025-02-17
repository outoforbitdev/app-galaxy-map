TRUNCATE solar_systems CASCADE;
COPY solar_systems FROM '/data/db/solar-systems.csv' DELIMITER ',' HEADER;
COPY spacelanes FROM '/data/db/spacelanes.csv' DELIMITER ',' HEADER;
