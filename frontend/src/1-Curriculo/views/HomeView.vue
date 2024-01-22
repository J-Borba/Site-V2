<template>
	<LoadingContainer>
		<section>
			<div class="d-flex flex-column justify-content-center align-items-center gap-5 text-center text">
				<div class="d-flex align-items-center gap-2">
					<div class="d-flex flex-column justify-content-center background-circle-spin">
						<a class="opacity-100" href="https://www.linkedin.com/in/joao-borba27/" target="_blank">
							<img id="MyFoto" src="@/0-Global/assets/MyFotoCircle.png" alt="Foto Joao Borba" />
						</a>
					</div>
					<h1 class="secondary">João Victor Borba</h1>
				</div>

				<div class="d-flex flex-column gap-3 text-start w-75">
					<p>
						Estudante de Ciências da Computação na
						<strong>Universidade do Estado do Rio de Janeiro (UERJ)</strong>
					</p>
					<p>
						Desenvolvedor .NET na
						<a href="https://www.tiplan.com.br/" target="_blank">TIPLAN</a>
					</p>
					<p>Amo desenvolver, e busco mais conhecimento a cada dia</p>
					<p>Confira meus <router-link to="/curriculo/certificados">certificados</router-link> 😉</p>
				</div>

				<div class="more-icon-grid-container">
					<p class="h4 mb-3">Mais sobre mim</p>
					<font-awesome-icon class="primary" :icon="['far', 'hand-point-down']" bounce size="2xl" />
				</div>
			</div>
		</section>

		<ExperienciaGrid />
		<FormacaoGrid />
		<SkillsGrid />
		<GithubCardsGrid />
	</LoadingContainer>
</template>

<script setup>
	import GithubCardsGrid from '../components/Grids/GithubCardsGrid.vue';
	import ExperienciaGrid from '../components/Grids/ExperienciaGrid.vue';
	import FormacaoGrid from '../components/Grids/FormacaoGrid.vue';
	import SkillsGrid from '../components/Grids/SkillsGrid.vue';
	import LoadingContainer from '../../0-Global/components/LoadingContainer.vue';

	import '../style/global.scss';
</script>

<style lang="scss" scoped>
	.background-circle-spin {
		position: relative;

		width: 10.5rem;
		aspect-ratio: 1;
		border-radius: 50%;

		overflow: hidden;
		&::before {
			content: '';

			position: absolute;
			z-index: -1;
			inset: -10px 40px;
			background: linear-gradient(315deg, var(--secondary), var(--primary));

			transition: 1s;
			animation: spin 10s linear infinite;
		}
		&:hover::before {
			transition: 300ms;
			inset: -10px 0;
		}
	}
	#MyFoto {
		width: 10rem;
		aspect-ratio: 1;
		border-radius: 50%;
	}
	@keyframes spin {
		from {
			transform: rotate(0deg);
		}
		to {
			transform: rotate(360deg);
		}
	}
	@media only screen and (max-width: 1168px) {
		.background-circle-spin {
			width: 7rem;
			&::before {
				inset: -10px 20px;
			}
		}
		#MyFoto {
			width: 6.5rem;
		}
	}
</style>
