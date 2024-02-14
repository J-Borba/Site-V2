<template>
  <section>
    <div class="d-flex flex-column align-items-center w-75 gap-3">
      <form class="d-flex flex-column gap-3 w-75 text-md-start text-center"  @submit.prevent="handleSubmit">
        <div class="d-flex flex-column gap-3" v-if="hasCadastro">
          <div class="d-flex flex-column gap-1 text-center">
            <h1 class="h3 mb-3 fw-normal">Seja Bem-vindo!</h1>
            <div class="d-flex flex-column gap-2">
              <div class="form-floating">
                <input type="email" class="form-control" id="floatingInput" placeholder="name@example.com" required />
                <label for="floatingInput">Email</label>
              </div>
              <div class="form-floating">
                <input type="password" class="form-control" id="floatingPassword" placeholder="Password" required />
                <label for="floatingPassword">Senha</label>
              </div>
            </div>
          </div>
          <div class="d-flex flex-column gap-2 w-100">
            <button class="btn btn-primary border-0 bg-primary py-2" type="submit">Entrar</button>
            <p class="d-flex w-100 justify-content-between">
              <a href="#" @click="handleCadastroClick">Criar conta</a>
              <a href="#" @click="handleEsqueciSenha">Esqueci minha senha</a>
            </p>
          </div>
        </div>
        <div class="d-flex flex-column gap-3" v-else>
          <div class="d-flex flex-column gap-1 text-center">
            <h1 class="h3 mb-3 fw-normal">Cadastre-se</h1>
            <div class="d-flex flex-column gap-2">
              <div class="form-floating">
                <input type="email" class="form-control" id="floatingInput" placeholder="name@example.com" required />
                <label for="floatingInput">Email</label>
              </div>
              <div class="form-floating">
                <input type="text" class="form-control" id="floatingUsername" placeholder="Username" required />
                <label for="floatingUsername">Nome de Usuário</label>
              </div>
              <div class="form-floating">
                <input type="password" class="form-control" id="floatingPassword" placeholder="Password" required />
                <label for="floatingPassword">Senha</label>
              </div>
              <div class="form-floating">
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
            <button class="btn btn-primary border-0 bg-primary py-2" type="submit">Criar Conta</button>
            <p>
              <a href="#" @click="handleCadastroClick">Já possuo cadastro</a>
            </p>
          </div>
        </div>
      </form>
    </div>
  </section>
</template>

<script setup>
  import '../style/global.scss';
  import { ref } from 'vue';
  import { myApi } from '@/0-Global/services/api';

  const handleCadastroClick = () => {
    hasCadastro.value = !hasCadastro.value;
  };

  const handleEsqueciSenha = () => {
    console.log('Esqueci minha senha');
  };

  const hasCadastro = ref(true);

  const handleSubmit = async () => {
    if (hasCadastro.value) {
      await myApi
        .post('User/Login', {
          email: 'joao@gmail.com',
          password: 'Teste@123!',
        })
        .then((response) => {
          console.log(response);
        })
        .catch((error) => {
          console.log(error);
        });
    } else {
      await myApi
        .post('User/Signin', {
          email: 'teste@gmail.com',
          userName: 'testeVue',
          password: 'Teste@123!',
          passwordConfirmation: 'Teste@123!',
        })
        .then((response) => {
          console.log(response);
        })
        .catch((error) => {
          console.log(error);
        });
    }
  };
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
