<template>
  <section>
    <div class="d-flex flex-column align-items-center text-center gap-5">
      <p class="h2 primary">Skills</p>
      <div class="d-flex justify-content-center flex-wrap gap-4 skills-wrapper">
        <div>
          <font-awesome-icon :icon="['fab', 'square-js']" size="2xl" class="icon" />
        </div>
        <div>
          <font-awesome-icon :icon="['fab', 'html5']" size="2xl" class="icon" />
        </div>
        <div>
          <font-awesome-icon :icon="['fab', 'sass']" size="2xl" class="icon" />
        </div>
        <div>
          <font-awesome-icon :icon="['fab', 'vuejs']" size="2xl" class="icon" />
        </div>
        <div>
          <font-awesome-icon :icon="['fab', 'react']" size="2xl" class="icon" />
        </div>
        <div>
          <font-awesome-icon :icon="['fab', 'java']" size="2xl" class="icon" />
        </div>
        <div>
          <font-awesome-icon :icon="['fab', 'git-alt']" size="2xl" class="icon" />
        </div>
        <div>
          <font-awesome-icon :icon="['fab', 'square-github']" size="2xl" class="icon" />
        </div>
      </div>

      <div class="desc-skills-container-container">
        <p class="h5 mb-2">Descrição das Skills</p>
        <router-link to="conhecimentos">
          <font-awesome-icon class="secondary" :icon="['fas', 'circle-info']" size="xl" />
        </router-link>
      </div>
    </div>
  </section>
</template>

<script setup>
import '../../style/global.scss';
</script>

<style lang="scss" scoped>
@media only screen and (max-width: 1168px) {
  .skills-wrapper {
    gap: 1.2rem;
    padding: 2rem 0;
    div {
      width: 20%;
    }
  }
  .icon {
    font-size: 2rem !important;
  }
}
</style>
