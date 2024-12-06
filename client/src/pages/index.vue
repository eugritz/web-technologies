<script setup lang="ts">
import CarCard from '~/components/CarCard.vue';
import type { CarPreview } from '~/types/Car';

const route = useRoute();

const firm = ref(route.query.firm as string ?? "");
const model = ref(route.query.model as string ?? "");

const { data: rawCars } = await useFetch<any[]>(
  `/cars?`
  + `firm=${encodeURIComponent(firm.value.toLowerCase() as string)}`
  + `&model=${encodeURIComponent(model.value.toLowerCase() as string)}`, {
    baseURL: "http://localhost:5177/api",
    server: true,
  }
);

const cars = computed<CarPreview[] | undefined>(() => {
  const images = [
    "https://business-car.ru/upload/resize_cache/iblock/1eb/1500_900_2/mfswzdjcukdcxj94i6wh2sjbjc8ktjyx.jpg",
    "https://arendacar.ru/wp-content/uploads/2023/10/771f829ac6cbf1eb609622ac30f5513b-e1730530422286-1160x688.jpg",
    "https://strg1.autovsalone.ru/neofiles/serve-image/5eb917b0279e56099b576a68/1190x500/q90",
    "https://cdn.saloncentr.ru/uploads/blog/34/images/intro_text.jpg",
    "https://autopremiumgroup.ru/m/_versions/info/ford/mustang/2020/3_uRcDEHv_banner.jpg",
    "https://autopremiumgroup.ru/m/_versions/catalog/autos/2021/chevrolet_camaro_2021_kupe/1ls_coupe/2021_camaro_coupe_1ls_sw_1_image_series.jpg",
    "https://static3.car.ru/uploaded/catalog/2016/9/19/1151/cdbe5d474f5605aa6910205259ff071f.jpg",
    "https://s9.auto.drom.ru/photo/v2/iHeo1pKlu4QWCh8RupwF87UEyFNKTMOxNTfkPA8S_EN95LKU-xio5TLiRJiqsnh5xolZKaYMGphGZTGz/gen1200.jpg",
    "https://s.auto.drom.ru/i24264/r/photos/1429182/big_1608519.jpg",
    "https://iat.ru/uploads/origin/models/320015/1.webp",
    "https://npr.brightspotcdn.com/legacy/sites/wuwm/files/201903/altima2.jpg",
    "https://upload.wikimedia.org/wikipedia/commons/thumb/b/b6/2021_Tesla_Model_S_P2_Long_Range_front_right_view.jpg/640px-2021_Tesla_Model_S_P2_Long_Range_front_right_view.jpg",
    "https://cdn.motor1.com/images/mgl/wOM8R/s1/2020-porsche-911-carrera-4-front-three-quarter.jpg",
    "https://images.drive.ru/i/0/5e81f267ec05c45c7f000012.jpg",
    "https://www.tts.ru/upload/iblock/286/novyy-kia-stinger-1.jpg",
    "https://media.autoexpress.co.uk/image/private/s--X-WVjvBW--/f_auto,t_content-image-full-desktop@1/v1563182803/autoexpress/2019/04/01_3.jpg",
  ];

  return rawCars.value?.map((car) => (<CarPreview>{
    firm: car.firm,
    model: car.model,
    year: car.year,
    power: car.power,
    color: car.color,
    price: car.price,
    preview: images[Math.floor(Math.random() * images.length)],
  }));
});
</script>

<template>
  <main>
    <h1>
      Каталог
      <span class="shadow" aria-hidden="true">Каталог</span>
    </h1>
    <div class="car-catalog">
      <div class="car-catalog__list">
        <div v-if="cars?.length === 0">
          По вашему запросу ничего не найдено.
        </div>
        <CarCard v-for="car in cars" :car="car" />
      </div>
      <div class="car-catalog__filter-wrapper">
        <form class="car-catalog__filter">
          <input v-model="firm" name="firm" type="text" placeholder="Фирма" />
          <input v-model="model" name="model" type="text" placeholder="Модель" />
          <button type="submit">Применить</button>
        </form>
      </div>
    </div>
  </main>
</template>

<style lang="scss" scoped>
@use 'assets/scss/breakpoints' as *;
@use 'assets/scss/theme';
@use 'sass:color';

.car-catalog {
  display: flex;
  gap: 1rem;

  @include media-breakpoint-down(sm) {
    flex-direction: column-reverse;
  }

  @include media-breakpoint-up(xl) {
    width: 80%;
  }
}

.car-catalog__list {
  padding: 1rem;
  border-radius: 24px;
  gap: 1rem;
  background-color: #fafaff;

  @include media-breakpoint-down(md) {
    display: flex;
    flex-direction: column;
  }

  @include media-breakpoint-up(md) {
    display: grid;
    grid-template-columns: repeat(2, 1fr);
  }

  @include media-breakpoint-up(lg) {
    display: grid;
    grid-template-columns: repeat(3, 1fr);
  }
}

.car-catalog__filter {
  padding: 1rem;
  border-radius: 24px;
  display: flex;
  flex-direction: column;
  gap: 0.5rem;

  background-color: #fafaff;

  input {
    padding: 0.5rem;
    border: 3px solid theme.$primary;
    border-radius: 24px;
    font-size: 1rem;
    background-color: #ffffff;
  }

  button {
    padding: 0.5rem;
    border: 3px solid theme.$primary;
    border-radius: 24px;
    box-shadow: 0px 0px 30px 10px rgba(theme.$primary, 0.5);
    font-size: 1rem;
    cursor: pointer;

    color: #ffffff;
    background-color: theme.$primary;
  }
}
</style>
