import { NextResponse } from "next/server";

export async function GET(_request: Request) {
  const data = await fetch(`${process.env.API_URL}/planets`);
  const planets = await data.json();

  return new NextResponse(JSON.stringify(planets), {
    status: 200,
  });
}
