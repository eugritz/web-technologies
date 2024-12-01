<script lang="ts">
import CarCard from '~/components/CarCard.vue';
</script>

<template>
  <main>
    <h1>Каталог</h1>
    <div class="car-catalog">
      <CarCard />
      <CarCard />
      <CarCard />
      <CarCard />
    </div>
  </main>
</template>

<style lang="scss" scoped>
@use 'assets/css/breakpoints' as *;

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
