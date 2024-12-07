// https://nuxt.com/docs/api/configuration/nuxt-config
export default defineNuxtConfig({
  compatibilityDate: '2024-11-01',
  css: ['~/assets/scss/main.scss', 'vue-final-modal/style.css'],
  devtools: { enabled: true },
  modules: ["nuxt-svgo"],
  srcDir: "src/",
});
