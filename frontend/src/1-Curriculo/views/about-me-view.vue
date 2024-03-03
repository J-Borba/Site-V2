<script setup lang="ts">
  import { faCircleInfo } from '@fortawesome/free-solid-svg-icons';
</script>

<template>
  <article>
    <section class="w-75 text-center">
      <span class="d-flex align-items-center gap-2">
        <p class="h2 color-secondary">Última Experiência</p>
        <router-link to="/experiencias">
          <font-awesome-icon class="icon" :icon="faCircleInfo" size="lg" />
        </router-link>
      </span>

      <iframe
        class="rounded-2 w-75"
        src="https://www.google.com/maps/embed?pb=!1m14!1m8!1m3!1d14700.985908340814!2d-43.178263!3d-22.904278!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x997f5fdc3e9565%3A0xe09773af0e23b78e!2sTIPLAN!5e0!3m2!1spt-BR!2sbr!4v1709422911840!5m2!1spt-BR!2sbr"
        width="400"
        height="200"
        style="border: 0"
        loading="lazy"
        referrerpolicy="no-referrer-when-downgrade" />

      <div>
        <a class="fs-5 color-secondary" href="https://www.tiplan.com.br/" target="_blank"> TIPLAN </a>
        <p class="color-blue-100">Desenvolvedor de Software</p>
        <p>Out/2023 - Atualmente</p>
      </div>
    </section>
  </article>
</template>

<style scoped lang="scss">
  article {
    display: flex;
    flex: 1;

    width: 100%;

    justify-content: center;
    align-items: center;
  }
  section {
    display: flex;
    flex-direction: column;

    align-items: center;
    gap: 1rem;
  }
</style>
