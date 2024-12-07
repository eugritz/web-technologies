<script setup lang="ts">
import { VueFinalModal } from 'vue-final-modal';

import EditIcon from '~/assets/svg/edit.svg';
import DeleteIcon from '~/assets/svg/delete.svg';

import type { Car, CarPreview } from '~/types/Car';

const props = defineProps<{
  car: CarPreview;
}>();

const emit = defineEmits<{
  (e: 'afterDelete'): void;
  (e: 'afterEdit', car: Car): void;
}>();

const loading = ref(false);
const deleteModalOpen = ref(false);
const editModalOpen = ref(false);
const title = computed(() => `${props.car.firm} ${props.car.model} ${props.car.year}`);

const firm = ref('');
const model = ref('');
const year = ref('0');
const power = ref('0');
const color = ref('');
const price = ref('0');

const error = ref<string | null>(null);

const closeDeleteModal = () => {
  deleteModalOpen.value = false;
  loading.value = false;
};

const deleteCar = async () => {
  loading.value = true;
  try {
    await $fetch(`/api/cars/${props.car.id}`, {
      method: "DELETE",
    });
    emit("afterDelete");
  } finally {
    closeDeleteModal();
  }
};

const openEditModal = () => {
  editModalOpen.value = true;

  firm.value = props.car.firm;
  model.value = props.car.model;
  year.value = props.car.year.toString();
  power.value = props.car.power.toString();
  color.value = props.car.color;
  price.value = props.car.price.toString();
};

const closeEditModal = () => {
  editModalOpen.value = false;
  loading.value = false;
  error.value = null;

  firm.value = '';
  model.value = '';
  year.value = '0';
  power.value = '0';
  color.value = '';
  price.value = '0';
};

const editCar = async (e: Event) => {
  e.preventDefault();
  try {
    const validYear = parseInt(year.value, 10);
    if (isNaN(validYear)) {
      error.value = "Неправильно задан год!";
      return;
    }

    const validPower = parseInt(power.value, 10);
    if (isNaN(validPower)) {
      error.value = "Неправильно задана мощность!";
      return;
    }

    const validPrice = parseInt(price.value, 10);
    if (isNaN(validPrice)) {
      error.value = "Неправильно задана цена!";
      return;
    }

    const car: Car = {
      id: props.car.id,
      firm: firm.value,
      model: model.value,
      year: validYear,
      power: validPower,
      color: color.value,
      price: validPrice,
    };

    loading.value = true;
    await $fetch(`/api/cars/${props.car.id}`, {
      method: "PUT",
      body: car,
    });

    emit("afterEdit", car);
    closeEditModal();
  } catch {
    closeEditModal();
  }
};
</script>

<template>
  <div class="car-catalog__list__car-card">
    <div class="car-card__preview">
      <div class="car-card__preview__image">
        <img :src="car.preview" />
      </div>
      <div class="car-card__preview__title">
        {{ title }}
      </div>
    </div>
    <div class="car-card__controls">
      <button title="Удалить" @click="deleteModalOpen = true">
        <DeleteIcon />
      </button>
      <button title="Изменить" @click="openEditModal">
        <EditIcon />
      </button>
    </div>
    <div class="car-card__decoration"></div>
    <VueFinalModal
      v-model="deleteModalOpen"
      class="modal"
      content-class="modal__content"
      v-on:closed="closeDeleteModal"
    >
      <span>
        Вы уверены, что хотите удалить <b>{{ title }}?</b>
      </span>
      <div class="modal__controls">
        <button
          :disabled="loading"
          @click="deleteModalOpen = false"
        >
          Нет
        </button>
        <span
          style="position: relative; display: flex; justify-content: center;"
        >
          <Spinner
            style="position: absolute; margin-top: 1px; margin-right: 1px;"
            v-if="loading"
          />
          <button
            :style="{ color: '#ff0000', opacity: loading ? '0%' : '100%' }"
            @click="deleteCar"
          >
            Да
            <DeleteIcon />
          </button>
        </span>
      </div>
    </VueFinalModal>
    <VueFinalModal
      v-model="editModalOpen"
      class="modal"
      content-class="modal__content edit-car-modal"
      v-on:closed="closeEditModal"
    >
      <form class="edit-car-modal__form" @submit="editCar" @change="error = null">
        <input v-model="firm" name="firm" type="text" placeholder="Фирма" />
        <input v-model="model" name="model" type="text" placeholder="Модель" />
        <input v-model="year" name="year" type="text" placeholder="Год" />
        <input v-model="power" name="power" type="text" placeholder="Мощность" />
        <input v-model="color" name="color" type="text" placeholder="Цвет" />
        <input v-model="price" name="price" type="text" placeholder="Цена" />
        <span v-if="error !== null" class="form__error">{{ error }}</span>
        <div class="modal__controls">
          <button
            :disabled="loading"
            type="button"
            @click="editModalOpen = false"
          >
            Закрыть
          </button>
          <span
            style="position: relative; display: flex; justify-content: center;"
          >
            <Spinner
              style="position: absolute; margin-top: 1px; margin-right: 1px;"
              v-if="loading"
            />
            <button
              :style="{ opacity: loading ? '0%' : '100%' }"
              type="submit"
              @click="editCar"
            >
              Изменить
              <EditIcon />
            </button>
          </span>
        </div>
      </form>
    </VueFinalModal>
  </div>
</template>

<style lang="scss">
@use 'assets/scss/breakpoints' as *;

.edit-car-modal {
  @include media-breakpoint-up(sm) {
    min-width: 400px;
  }
}
</style>

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
    z-index: 1;
  }
}

.car-card__preview__image {
  display: flex;

  > img {
    width: 100%;
    object-fit: cover;
    transform: translateY(25%);
  }
}

.car-card__preview__title {
  position: absolute;
  display: flex;
  pointer-events: none;
  z-index: 2;

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

.edit-car-modal__form {
  display: flex;
  flex-direction: column;
  gap: 0.5rem;

  input {
    padding: 0.5rem;
    border: 3px solid theme.$primary;
    border-radius: 24px;
    font-size: 1rem;
    background-color: #ffffff;
  }

  .form__error {
    color: #ff0000;
  }
}
</style>
