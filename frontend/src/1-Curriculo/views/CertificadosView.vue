<template>
	<section>
		<div>
			<h1 class="h3 mb-5 text-center">Certificados</h1>
			<div class="d-flex flex-column gap-5 border border-secondary rounded-2 p-3 bg-emphasis">
				<div class="d-flex flex-column gap-3" v-for="company in companies" :key="company.id">
					<div class="d-flex align-items-center gap-2">
						<img :src="company.icon" alt="company icon" />
						<p class="h5">{{ company.name }}</p>
					</div>
					<div class="d-flex flex-column gap-1 ms-2">
						<div v-for="certificate in company.certificates" :key="certificate.id">
							<a class="secondary" :href="certificate.url">
								<font-awesome-icon :icon="['fas', 'medal']" /> {{ certificate.title }}
							</a>
						</div>
					</div>
				</div>
			</div>
		</div>
	</section>
</template>

<script setup>
	import '../style/global.scss';
	import aluraIcon from '../assets/alura.jpeg';
	import udemyIcon from '../assets/udemy.jpeg';

	const companies = [
		{
			id: 2,
			name: 'Alura',
			icon: aluraIcon,
			certificates: [
				{
					id: 1,
					title: 'HTTP: entendendo a web por baixo dos panos',
					url: 'https://cursos.alura.com.br/certificate/6251e3fc-d35f-4e7c-9c30-b5d1ab07db59',
				},
			],
		},
		{
			id: 1,
			name: 'Udemy',
			icon: udemyIcon,
			certificates: [
				{
					id: 3,
					title: 'C# COMPLETO Programação Orientada a Objetos + Projetos',
					url: 'https://www.udemy.com/certificate/UC-855c52b9-7d99-4268-814a-b518bef8c295/',
				},
				{
					id: 2,
					title: 'The Complete 2023 Web Development Bootcamp',
					url: 'https://www.udemy.com/certificate/UC-3e28fcf8-faed-4241-9940-002f7c1afb63/',
				},
				{
					id: 1,
					title: 'Power BI Completo - Do Básico ao Avançado',
					url: 'https://www.udemy.com/certificate/UC-d2e1559c-5b7a-45b7-9168-e1db9a8aa03d/',
				},
			],
		},
	];
</script>

<style scoped>
	img {
		width: 1.5rem;
		aspect-ratio: 1;
	}
</style>
