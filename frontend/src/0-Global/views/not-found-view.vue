<script setup lang="ts"></script>

<template>
  <section>
    <h1>404</h1>
    <h2>
      <span> Oops, esta página não existe! </span>
      <span> Selecione um caminho pelo cabeçalho.</span>
    </h2>
  </section>
</template>

<style scoped lang="scss">
  section {
    display: flex;
    flex: 1;
    flex-direction: column;

    width: 100%;

    align-items: center;
    justify-content: center;

    & > h1 {
      font-size: 12rem;
      font-weight: bold;

      color: var(--primary);
    }
    & > h2 {
      display: flex;

      flex-direction: column;
      text-align: center;

      font-size: 1.5rem;
      color: var(--secondary);
    }
  }
</style>
