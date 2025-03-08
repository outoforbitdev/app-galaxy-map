"use client";

import GalaxyMap from "@outoforbitdev/galaxy-map";
import { useRouter } from "next/router";
import { Fragment, useEffect, useState } from "react";

export default function Map() {
  const router = useRouter();
  return (
    router.push("/map-legends")
  );
}
