<template>
  <div class="car-catalog__car-card">
    <p class="car-card__desc">
      Hello
    </p>
    <div class="car-card__controls">
      <button>asd</button>
      <button>asd</button>
    </div>
  </div>
</template>

<style lang="scss" scoped>
.car-catalog__car-card {
  background-color: #ffffff;
  border-radius: 24px;
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
    font-size: 1rem;
    cursor: pointer;

    transition: 0.5s;

    &:first-child {
      padding-left: 1.5rem;
      border-radius: 0 0 0 24px;
    }

    &:last-child {
      padding-right: 1.5rem;
      border-radius: 0 0 24px 0;
    }

    &:hover {
      flex: 2;
      transition: 0.5s;
    }
  }
}
</style>
