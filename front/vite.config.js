import { fileURLToPath, URL } from "node:url"
import { defineConfig } from "vite"
import vue from "@vitejs/plugin-vue"

export default defineConfig({
	plugins  : [vue()],
	resolve  : {
		alias: {
			"@": fileURLToPath(new URL("./src", import.meta.url))
		}
	},
	server   : {
		proxy: {
			"/api": {
				target      : "http://localhost:5015",
				changeOrigin: true,
				secure      : false,
				//rewrite     : (path) => path.replace(/^\/api/, "")
				/*
				configure: (proxy, options) => {
					proxy.on('proxyReq', (proxyReq, req, res) => {
						console.log(`proxyReq: ${req.method} ${req.url} -> ${options.target}${proxyReq.path}`)
					})
					proxy.on('proxyRes', (proxyRes, req, res) => {
						console.log(`proxyRes: ${proxyRes.statusCode} de ${req.url}`)
					})
					proxy.on('error', (err, req, res) => {
						console.error(`error: ${err.message}`)
					})
				}
				*/
			}
		}
	},
	publicDir: "public"
})