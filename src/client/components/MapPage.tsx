"use client";

import { fetchMap } from "@/lib/FetchMap";
import GalaxyMap from "@outoforbitdev/galaxy-map";
import { Fragment, useEffect, useState } from "react";

interface IMapPageProps {
  instanceId: string;
}

export default function MapPage(props: IMapPageProps) {
  const [planets, setPlanets] = useState([]);
  const [spacelanes, setSpacelanes] = useState([]);
  const [exampleCustomOption, setExampleCustomOption] = useState(true);
  useEffect(() => {
    fetchMap(props.instanceId, setPlanets, setSpacelanes);
  }, []);

  const customOptions = [
    {
      currentValue: exampleCustomOption,
      setValue: setExampleCustomOption,
      label: "example custom option",
      inputType: "checkbox",
    },
  ];

  return (
    <Fragment>
      <GalaxyMap
        planets={planets}
        spacelanes={spacelanes}
        dimensions={{ minX: -12000, maxX: 12000, minY: -12000, maxY: 12000 }}
        zoom={{ initial: 80, min: 0 }}
        mapOptions={{ customOptions: customOptions }}
      />
      <p>
        Star Wars and all associated names are copyright Lucasfilm and Disney.
      </p>
    </Fragment>
  );
}
