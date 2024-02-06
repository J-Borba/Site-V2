<template>
  <section>
    <div class="d-flex justify-content-center align-items-center w-75">
      <form class="d-flex flex-column gap-3 w-75" v-if="!hasCadastro">
        <div class="d-flex flex-column gap-1 text-center">
          <h1 class="h3 mb-3 fw-normal">Cadastre-se</h1>
          <div class="d-flex flex-column gap-2">
            <div class="form-floating">
              <input type="email" class="form-control" id="floatingInput" placeholder="name@example.com" required />
              <label for="floatingInput">Email address</label>
            </div>
            <div class="form-floating">
              <input type="text" class="form-control" id="floatingUsername" placeholder="Username" required />
              <label for="floatingUsername">UserName</label>
            </div>
            <div class="form-floating">
              <input type="password" class="form-control" id="floatingPassword" placeholder="Password" required />
              <label for="floatingPassword">Password</label>
            </div>
            <div class="form-floating">
              <input
                type="password"
                class="form-control"
                id="floatingPasswordConfirmation"
                placeholder="PasswordConfirmation"
                required />
              <label for="floatingPasswordConfirmation">Confirm your Password</label>
            </div>
          </div>
        </div>

        <button class="btn btn-primary border-0 bg-primary w-100 py-2" type="submit">Sign up</button>
        <a href="#" @click="handleCadastroClick">Já possuo cadastro.</a>
      </form>
      <form class="d-flex flex-column gap-3 w-75" v-else>
        <div class="d-flex flex-column gap-1 text-center">
          <h1 class="h3 mb-3 fw-normal">Bem vindo novamente!</h1>
          <div class="d-flex flex-column gap-2">
            <div class="form-floating">
              <input type="email" class="form-control" id="floatingInput" placeholder="name@example.com" required />
              <label for="floatingInput">Email address</label>
            </div>
            <div class="form-floating">
              <input type="password" class="form-control" id="floatingPassword" placeholder="Password" required />
              <label for="floatingPassword">Password</label>
            </div>
          </div>
        </div>

        <button class="btn btn-primary border-0 bg-primary w-100 py-2" type="submit">Sign in</button>
        <a href="#" @click="handleCadastroClick">Criar conta.</a>
      </form>
    </div>
  </section>
</template>

<script setup>
  import { ref } from 'vue';
  import '../style/global.scss';

  const handleCadastroClick = () => {
    hasCadastro.value = !hasCadastro.value;
  };

  const hasCadastro = ref(false);
</script>

<style lang="scss">
  form {
    animation: 1s linear appear !important;
  }

  .form-control {
    background-color: transparent !important;
  }
  .form-floating {
    * {
      color: var(--text) !important;
    }
  }
  label {
    &::after {
      background-color: transparent !important;
    }
  }
</style>
