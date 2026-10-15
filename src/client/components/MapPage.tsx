"use client";

import {
  GalaxyMap,
  type ISpacelane,
  type ISystem,
} from "@outoforbitdev/galaxy-map";
import { Fragment, useEffect, useState } from "react";
import DateSelect from "./DateSelect";
import styles from "../app/page.module.css";
import { Infobox, ISolarSystem } from "./Infobox";
import { ILegendEntry, Legend } from "./Legend";
import { toCssColor } from "./mapColors";

/** A system as returned by the map API. */
interface IApiSystem {
  id: string;
  name: string;
  x: number;
  y: number;
  color: string;
}

/** A spacelane segment as returned by the map API. */
interface IApiSegment {
  name: string;
  xOne: number;
  yOne: number;
  xTwo: number;
  yTwo: number;
  color: string;
}

/** Converts API systems to map systems, keeping the API's priority order. */
function toSystems(systems: IApiSystem[]): ISystem[] {
  return systems.map((s) => ({
    id: s.id,
    name: s.name,
    position: { x: s.x, y: s.y },
    color: toCssColor(s.color),
  }));
}

/** Converts API segments to single-segment lanes, keeping priority order. */
function toSpacelanes(segments: IApiSegment[]): ISpacelane[] {
  return segments.map((s, i) => ({
    id: `lane-${i}`,
    name: s.name,
    segments: [
      {
        id: `lane-${i}-0`,
        origin: { x: s.xOne, y: s.yOne },
        destination: { x: s.xTwo, y: s.yTwo },
        color: toCssColor(s.color),
      },
    ],
  }));
}

/** The area the galaxy occupies, used as the map's pan bounds. */
const GALAXY_BOUNDS = {
  min: { x: -12000, y: -12000 },
  max: { x: 12000, y: 12000 },
};

interface IMapPageProps {
  instanceId: string;
}

export default function MapPage(props: IMapPageProps) {
  const [systems, setSystems] = useState<ISystem[]>([]);
  const [spacelanes, setSpacelanes] = useState<ISpacelane[]>([]);
  const [legendEntries, setLegendEntries] = useState<ILegendEntry[]>([]);
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
    setSystems(toSystems(json_data.systems));
    setSpacelanes(toSpacelanes(json_data.spacelanes));
    setLegendEntries(json_data.legend);
  };
  useEffect(() => {
    fetchMap();
  }, [date]);

  const hasSelection = selectedSystem !== null;

  return (
    <Fragment>
      <div className={styles.map_container}>
        <div className={styles.map_canvas}>
          <GalaxyMap
            systems={systems}
            spacelanes={spacelanes}
            bounds={GALAXY_BOUNDS}
            minZoom={0.01}
            maxZoom={0.5}
            selected={
              selectedSystem ? { kind: "system", id: selectedSystem.id } : null
            }
            onSelect={async (event) => {
              if (event?.kind === "system") {
                setSelectedSystem(
                  await getSystem(props.instanceId, date, event.system.id),
                );
              } else {
                clearSelection();
              }
            }}
          />
        </div>
        <div className={styles.map_options}>
          <DateSelect
            onChange={(date) => {
              clearSelection();
              setDate(date);
            }}
            defaultValue={date}
          />
          <Legend entries={legendEntries} />
        </div>
        {hasSelection ? (
          <div className={styles.infobox_container}>
            <Infobox
              clearSelection={clearSelection}
              solarSystem={selectedSystem}
            />
          </div>
        ) : null}
      </div>
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
