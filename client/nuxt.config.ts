import { extendWebpackConfig } from "nuxt/kit"

// https://nuxt.com/docs/api/configuration/nuxt-config
export default defineNuxtConfig({
  compatibilityDate: '2024-11-01',
  css: ['~/assets/scss/main.scss'],
  devtools: { enabled: true },
  modules: ["nuxt-svgo"],
  srcDir: "src/",
})
