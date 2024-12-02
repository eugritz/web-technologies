<script setup lang="ts">
import CarCard from '~/components/CarCard.vue';
import type Car from '~/types/Car';

const car: Car = {
  firm: "Skoda",
  model: "Rapid",
  color: "Silver",
  power: 999,
  price: 999,
  year: 2015,
};
</script>

<template>
  <main>
    <h1>
      Каталог
      <h1 class="shadow" aria-hidden="true">Каталог</h1>
    </h1>
    <div class="car-catalog">
      <CarCard :car="car" />
    </div>
  </main>
</template>

<style lang="scss" scoped>
@use 'assets/scss/breakpoints' as *;

.car-catalog {
  padding: 1rem;
  border-radius: 24px;
  background-color: #fafaff;

  @include media-breakpoint-up(sm) {
    display: grid;
    grid-template-columns: repeat(2, 1fr);
    gap: 1rem;
  }

  @include media-breakpoint-up(md) {
    display: grid;
    grid-template-columns: repeat(3, 1fr);
    gap: 1rem;
  }

  @include media-breakpoint-up(xl) {
    width: 80%;
    display: grid;
    grid-template-columns: repeat(4, 1fr);
    gap: 1rem;
  }

  @include media-breakpoint-down(sm) {
    height: 100%;
  }
}
</style>
