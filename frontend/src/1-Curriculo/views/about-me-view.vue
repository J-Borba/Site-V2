<script setup lang="ts">
  import { faCircleInfo } from '@fortawesome/free-solid-svg-icons';
  import GoogleMaps from '../components/google-maps.vue';
</script>

<template>
  <section>
    <span class="d-flex justify-content-center align-items-center gap-2">
      <p class="h2 color-secondary">Última Experiência</p>
      <router-link to="/experiencias">
        <font-awesome-icon class="icon" :icon="faCircleInfo" size="lg" />
      </router-link>
    </span>

    <GoogleMaps
      src="https://www.google.com/maps/embed?pb=!1m14!1m8!1m3!1d14700.985908340814!2d-43.178263!3d-22.904278!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x997f5fdc3e9565%3A0xe09773af0e23b78e!2sTIPLAN!5e0!3m2!1spt-BR!2sbr!4v1709422911840!5m2!1spt-BR!2sbr" />

    <div>
      <a class="fs-5 color-secondary" href="https://www.tiplan.com.br/" target="_blank"> TIPLAN </a>
      <p class="color-blue-100">Desenvolvedor de Software</p>
      <p>Out/2023 - Atualmente</p>
    </div>
  </section>
  <section>
    <p class="h2 color-secondary">Formação Acadêmica</p>

    <GoogleMaps
      src="https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d58800.952966794444!2d-43.31234995136716!3d-22.9111739!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x997e66c5f330ad%3A0x874ed5a98cf472d1!2sUniversidade%20do%20Estado%20do%20Rio%20de%20Janeiro!5e0!3m2!1spt-BR!2sbr!4v1709481881887!5m2!1spt-BR!2sbr" />

    <div>
      <a class="fs-5 color-secondary" href="https://www.uerj.br/" target="_blank"> UERJ </a>
      <p>Set/2020 - Cursando</p>
    </div>
  </section>
</template>

<style scoped lang="scss">
  section {
    display: flex;
    flex-direction: column;

    padding: 5rem 0;

    justify-content: center;
    align-items: center;
    text-align: center;

    gap: 2rem;
  }
</style>
