<template>
  <section>
    <div class="d-flex flex-column align-items-center w-75 gap-3">
      <form class="d-flex flex-column gap-3 w-75 text-md-start text-center">
        <h1 class="h3 mb-3 fw-normal">{{ hasCadastro ? 'Seja Bem Vindo!' : 'Cadastre-se' }}</h1>
        <div class="d-flex flex-column gap-1 text-center">
          <div class="d-flex flex-column gap-2">
            <div class="form-floating">
              <input type="email" class="form-control" id="floatingInput" placeholder="name@example.com" required />
              <label for="floatingInput">Email</label>
            </div>
            <div class="form-floating" v-if="!hasCadastro">
              <input type="text" class="form-control" id="floatingUsername" placeholder="Username" required />
              <label for="floatingUsername">Nome de Usuário</label>
            </div>
            <div class="form-floating">
              <input type="password" class="form-control" id="floatingPassword" placeholder="Password" required />
              <label for="floatingPassword">Senha</label>
            </div>
            <div class="form-floating" v-if="!hasCadastro">
              <input
                type="password"
                class="form-control"
                id="floatingPasswordConfirmation"
                placeholder="PasswordConfirmation"
                required />
              <label for="floatingPasswordConfirmation">Confirme sua Senha</label>
            </div>
          </div>
        </div>
        <div class="d-flex flex-column gap-2 w-100">
          <button class="btn btn-primary border-0 bg-primary py-2" type="submit">
            {{ hasCadastro ? 'Entrar' : 'Criar Conta' }}
          </button>
          <p>
            <a href="#" @click="handleCadastroClick"> {{ hasCadastro ? 'Criar Conta.' : 'Já possuo cadastro.' }}</a>
          </p>
        </div>
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
  .form-control {
    background-color: transparent !important;
  }
  .form-floating {
    * {
      color: var(--text) !important;
    }
  }
  form {
    animation: 1s linear appear !important;
  }
  label {
    &::after {
      background-color: transparent !important;
    }
  }
  input:focus {
    box-shadow: 0px 0px 5px 2px var(--primary) !important;
    border-color: var(--secondary) !important;
  }
</style>
