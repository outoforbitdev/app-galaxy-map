import { NextResponse } from "next/server";

export async function GET(request: Request) {
  const { searchParams } = new URL(request.url);
  const instanceId = searchParams.get("instanceId");
  const date = searchParams.get("date");
  const systemId = searchParams.get("systemId");

  console.log("fetching system", instanceId, date, systemId);

  const data = await fetch(
    `${process.env.API_URL}/system/instance/${instanceId}/date/${date}/system/${systemId}`,
  );
  console.log(data);
  if (!data.ok) {
    return new NextResponse(data.statusText, {
      status: data.status,
    });
  }
  const map = await data.json();

  return new NextResponse(JSON.stringify(map), {
    status: 200,
  });
}
