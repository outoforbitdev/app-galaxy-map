import { NextResponse } from "next/server";

export async function GET(_request: Request) {
  const data = await fetch(`${process.env.API_URL}/spacelanes`);
  const spacelanes = await data.json();

  return new NextResponse(JSON.stringify(spacelanes), {
    status: 200,
  });
}
