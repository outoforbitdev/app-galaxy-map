/** @type {import('next').NextConfig} */
const nextConfig = {
  output: "standalone",
  async redirects() {
    return [
      {
        source: "/",
        destination: "/map-legends",
        permanent: false,
      },
    ];
  },
};

export default nextConfig;
