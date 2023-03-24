const PROXY_CONFIG = [
  {
    context: [
      "/Telemetry",
    ],
    target: "http://localhost:5248",
    secure: false
  }
]

module.exports = PROXY_CONFIG;
