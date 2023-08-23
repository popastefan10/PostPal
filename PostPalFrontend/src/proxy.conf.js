const PROXY_CONFIG = [
  {
    context: [
			"/weatherforecast",
			"/api/users"
    ],
    target: "https://localhost:7189",
    secure: false
  }
]

module.exports = PROXY_CONFIG;
