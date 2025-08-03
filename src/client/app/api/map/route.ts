import { NextResponse } from "next/server";

export async function GET(request: Request) {
  const { searchParams } = new URL(request.url);
  const instanceId = searchParams.get("instanceId");
  const date = searchParams.get("date");

  const data = await fetch(
    `${process.env.API_URL}/map/instance/${instanceId}/date/${date}`,
  );
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
