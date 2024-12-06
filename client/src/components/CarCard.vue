<script setup lang="ts">
import EditIcon from '~/assets/svg/edit.svg';
import DeleteIcon from '~/assets/svg/delete.svg';
import type { CarPreview } from '~/types/Car';

defineProps<{
  car: CarPreview,
}>();
</script>

<template>
  <div class="car-catalog__list__car-card">
    <div class="car-card__preview">
      <img :src="car.preview" />
      <div class="car-card__preview__title">
        {{ `${$props.car.firm} ${$props.car.model} ${$props.car.year}` }}
      </div>
    </div>
    <div class="car-card__controls">
      <button title="Удалить">
        <DeleteIcon />
      </button>
      <button title="Изменить">
        <EditIcon />
      </button>
    </div>
    <div class="car-card__decoration"></div>
  </div>
</template>

<style lang="scss" scoped>
@use 'assets/scss/theme';

$border-radius: 24px;

.car-catalog__list__car-card {
  position: relative;
  display: flex;
  flex-direction: column;
  border-radius: $border-radius;
  color: #ffffff;
  background-color: #ffffff;
  cursor: pointer;
}

.car-card__decoration {
  $padding: 6px;

  position: absolute;
  left: $padding;
  top: $padding;
  width: calc(100% - 2 * $padding);
  height: calc(100% - 2 * $padding);
  border-radius: $border-radius;

  pointer-events: none;
  z-index: 1;

  background-color: #ffffff33;
  filter: blur(6px);
}

.car-card__preview {
  position: relative;
  height: 100px;
  border-radius: $border-radius $border-radius 0 0;
  display: flex;
  flex-direction: column-reverse;
  overflow: hidden;

  &::before {
    content: '';
    position: absolute;
    width: 100%;
    height: 100%;
    border-radius: $border-radius $border-radius 0 0;
    background: linear-gradient(transparent 75%, theme.$primary);
  }

  > img {
    // width: 100%;
    height: 100%;
    object-fit: cover;
  }
}

.car-card__preview__title {
  position: absolute;
  display: flex;
  pointer-events: none;

  font-weight: 500;
  background-color: theme.$secondary;
}

.car-card__controls {
  display: flex;

  button {
    flex: 1;
    width: 100%;
    min-height: 2rem;
    border: none;

    color: #ffffff;
    background-color: #4e4eff;
    font-size: 1.2rem;
    cursor: pointer;

    transition: 0.5s;

    &:first-child {
      padding-left: 1.5rem;
      border-radius: 0 0 0 $border-radius;
    }

    &:last-child {
      padding-right: 1.5rem;
      border-radius: 0 0 $border-radius 0;
    }

    &:hover {
      flex: 2;
      transition: 0.5s;
    }
  }
}
</style>
