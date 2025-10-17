"use client";

import GalaxyMap from "@outoforbitdev/galaxy-map";
import { Fragment, useEffect, useState } from "react";
import DateSelect from "./DateSelect";
import styles from "../app/page.module.css";
import { Infobox, ISolarSystem } from "./Infobox";

interface IMapPageProps {
  instanceId: string;
}

export default function MapPage(props: IMapPageProps) {
  const [planets, setPlanets] = useState([]);
  const [spacelanes, setSpacelanes] = useState([]);
  const [legendEntries, setLegendEntries] = useState([]);
  const [date, setDate] = useState(-7900);
  const [selectedSystem, setSelectedSystem] = useState<ISolarSystem | null>(
    null,
  );
  const clearSelection = () => {
    setSelectedSystem(null);
  };

  const fetchMap = async function () {
    const data = await fetch(
      `/api/map?instanceId=${props.instanceId}&date=${date}`,
    );
    if (!data.ok) {
      console.error("Failed to fetch map data");
      return;
    }
    const json_data = await data.json();
    const planet_list = json_data.systems;
    const spacelane_list = json_data.spacelanes;
    const legend_entries = json_data.legend;

    setPlanets(planet_list);
    setSpacelanes(spacelane_list);
    setLegendEntries(legend_entries);
  };
  useEffect(() => {
    fetchMap();
  }, [date]);

  const customOptions = (
    <DateSelect
      onChange={(date) => {
        clearSelection();
        setDate(date);
      }}
      defaultValue={date}
    />
  );
  const hasSelection = selectedSystem !== null;

  return (
    <Fragment>
      <GalaxyMap
        planets={planets}
        spacelanes={spacelanes}
        dimensions={{ minX: -12000, maxX: 12000, minY: -12000, maxY: 12000 }}
        zoom={{ initial: 0.5, min: 0.25, max: 10 }}
        mapOptions={{
          planetVisibility: "show",
          customOptions: customOptions,
        }}
        legendEntries={legendEntries}
        className={styles.map_container}
        onPlanetSelect={async (planet) => {
          setSelectedSystem(await getSystem(props.instanceId, date, planet.id));
        }}
        selectedPlanetId={selectedSystem?.id || undefined}
      >
        {hasSelection ? (
          <Infobox
            clearSelection={clearSelection}
            solarSystem={selectedSystem}
          />
        ) : null}
      </GalaxyMap>
      <p>
        Star Wars and all associated names are copyright Lucasfilm and Disney.
      </p>
    </Fragment>
  );
}

async function getSystem(instanceId: string, date: number, systemId: string) {
  const data = await fetch(
    `/api/system?instanceId=${instanceId}&date=${date}&systemId=${systemId}`,
  );
  if (!data.ok) {
    throw new Error("Failed to fetch system data");
  }
  return await data.json();
}
