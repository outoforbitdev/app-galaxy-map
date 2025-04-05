"use client";

import GalaxyMap from "@outoforbitdev/galaxy-map";
import { Fragment, useEffect, useState } from "react";

interface IMapPageProps {
  instanceId: string;
}

export default function MapPage(props: IMapPageProps) {
  const [planets, setPlanets] = useState([]);
  const [spacelanes, setSpacelanes] = useState([]);
  const [exampleCustomOption, setExampleCustomOption] = useState(true);

  const fetchMap = async function () {
    const data = await fetch(`/api/map?instanceId=${props.instanceId}`);
    if (!data.ok) {
      console.error("Failed to fetch map data");
      return;
    }
    const json_data = await data.json();
    const planet_list = json_data.systems;
    const spacelane_list = json_data.spacelanes;

    setPlanets(planet_list);
    setSpacelanes(spacelane_list);
  };
  useEffect(() => {
    fetchMap();
  }, []);

  const customOptions = (
    <p>Example custom option</p>
  )

  return (
    <Fragment>
      <GalaxyMap
        planets={planets}
        spacelanes={spacelanes}
        dimensions={{ minX: -12000, maxX: 12000, minY: -12000, maxY: 12000 }}
        zoom={{ initial: 0.5, min: 0.3, max: 10 }}
        mapOptions={{
          planetVisibility: "show",
          customOptions: customOptions,
        }}
      />
      <p>
        Star Wars and all associated names are copyright Lucasfilm and Disney.
      </p>
    </Fragment>
  );
}
