<script setup lang="ts">
import { ModalsContainer } from 'vue-final-modal';
</script>

<template>
  <div>
    <NuxtRouteAnnouncer />
    <NuxtLayout>
      <header>
        <nav>
          <ul>
            <li class="home"><a href="/">Главная</a></li>
            <li><a href="/">Каталог</a></li>
            <li><a href="/help">Справка</a></li>
          </ul>
        </nav>
      </header>
      <NuxtPage />
      <ModalsContainer />
    </NuxtLayout>
  </div>
</template>

<style lang="scss" scoped>
@use 'assets/scss/breakpoints' as *;
@use 'assets/scss/theme';

nav {
  $padding-x: 1rem;
  $padding-y: 1rem;

  color: theme.$primary;
  font-family: sans-serif;
  font-size: 1.2rem;
  font-weight: 1000;
  letter-spacing: 1px;

  .home {
    @include media-breakpoint-down(sm) {
      display: none;
    }
  }

  ul {
    margin: 0;
    display: flex;
    list-style: none;

    @include media-breakpoint-down(sm) {
      padding: 0;
    }
  }

  li {
    padding: $padding-y 0;
  }

  a {
    position: relative;
    padding: $padding-y $padding-x;
    color: inherit;
    text-decoration: none;
  }

  a:hover {
    &::before {
      content: '';
      position: absolute;
      bottom: $padding-y - 0.1rem;
      height: 0.2rem;
      width: calc(100% - 2 * $padding-x);
      background-color: theme.$primary;

      animation-name: link-hover;
      animation-duration: 0.3s;

      @keyframes link-hover {
        from {
          width: 0;
        }

        to {
          width: calc(100% - 2 * $padding-x);
        }
      }
    }
  }
}
</style>
