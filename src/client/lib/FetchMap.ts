export async function fetchMap(
  instanceId: string,
  setPlanets: any,
  setSpacelanes: any,
) {
  const data = await fetch(`/api/map?instanceId=${instanceId}`);
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
