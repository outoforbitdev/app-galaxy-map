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
  }
  useEffect(() => {
    fetchMap();
  }, []);

  const customOptions = [
    {
      currentValue: exampleCustomOption,
      setValue: setExampleCustomOption,
      label: "example custom option",
      inputType: "checkbox",
    },
  ];
  console.log(planets);

  const mapOptions = {
    hideSpacelaneLabels: true,
    showAllPlanets: true,
    customOptions: customOptions,
  }

  return (
    <Fragment>
      <GalaxyMap
        planets={planets}
        spacelanes={spacelanes}
        dimensions={{ minX: -12000, maxX: 12000, minY: -12000, maxY: 12000 }}
        zoom={{ initial: .5, min: .3, max: 10 }}
        mapOptions={mapOptions}
      />
      <p>
        Star Wars and all associated names are copyright Lucasfilm and Disney.
      </p>
    </Fragment>
  );
}
