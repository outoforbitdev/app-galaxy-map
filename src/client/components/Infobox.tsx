import {
  Button,
  IChildlessComponentProps,
  Icons,
  Infobox as GenericInfobox,
  InfoboxSection,
  InfoboxTitle,
  InfoboxRow,
} from "@outoforbitdev/ood-react";
import styles from "../app/page.module.css";

export interface ISolarSystem {
  id: string;
  name: string;
  sector: string;
  region: string;
  x: number;
  y: number;
  planets: string[];
  government: string;
}

interface IInfoboxProps extends IChildlessComponentProps {
  solarSystem: ISolarSystem;
  clearSelection: () => void;
}

export function Infobox(props: IInfoboxProps) {
  return (
    <div className={styles.infobox_container}>
      <GenericInfobox id={styles.infobox}>
        <InfoboxTitle>
          {props.solarSystem.name}
          <Button
            className={styles.close_button + " ood-accent-block"}
            onClick={props.clearSelection}
          >
            <Icons.X className="ood-accent-block" />
          </Button>
        </InfoboxTitle>
        <InfoboxSection title="Astrographical Details">
          <InfoboxRow label="Coordinates">
            {props.solarSystem.x}, {props.solarSystem.y}
          </InfoboxRow>
          <InfoboxRow label="Sector">{props.solarSystem.sector}</InfoboxRow>
          <InfoboxRow label="Region">{props.solarSystem.region}</InfoboxRow>
          <InfoboxRow label="Planets">
            <ul>
              {props.solarSystem.planets.map((planet) => (
                <li key={planet}>{planet}</li>
              ))}
            </ul>
          </InfoboxRow>
          <InfoboxRow label="Government">
            {props.solarSystem.government}
          </InfoboxRow>
        </InfoboxSection>
      </GenericInfobox>
    </div>
  );
}
