import { zoomLevelToModifier } from "./GalaxyMap";

export interface ISpacelane {
  name: string;
  xOne: number;
  yOne: number;
  xTwo: number;
  yTwo: number;
  color: string;
  focusLevel: number;
}

interface ISpacelaneMapProps {
  spacelane: ISpacelane;
  centerX: number;
  centerY: number;
  zoomLevel: number;
}

export default function SpacelaneMap(props: ISpacelaneMapProps) {
  const zoomModifier = zoomLevelToModifier(props.zoomLevel);
  const spacelane = props.spacelane;
  const xOne = props.centerX + spacelane.xOne / zoomModifier;
  const yOne = props.centerY - spacelane.yOne / zoomModifier;
  const xTwo = props.centerX + spacelane.xTwo / zoomModifier;
  const yTwo = props.centerY - spacelane.yTwo / zoomModifier;
  const color = spacelane.color;
  const inFocus = spacelane.focusLevel >= zoomModifier;
  const strokeWidth = inFocus
    ? 3
    : zoomModifier - spacelane.focusLevel < 10
      ? 1
      : 0;

  return (
    <g fill={color} stroke={color}>
      <line x1={xOne} y1={yOne} x2={xTwo} y2={yTwo} strokeWidth={strokeWidth} />
    </g>
  );
}
