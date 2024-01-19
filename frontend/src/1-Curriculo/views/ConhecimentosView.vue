<template>
	<section>
		<div>
			<h1 class="h3 mb-5 text-center">Conhecimentos</h1>
			<div class="d-flex flex-column gap-5 border border-secondary rounded-2 p-3 bg-emphasis">
				<div class="" :id="skill.sigla" v-for="skill in skills" :key="skill.id">
					<p class="h5 primary mb-3">{{ skill.tipo }}</p>
					<div class="d-flex flex-column gap-3 small ms-3">
						<p v-for="line in skill.descricao" :key="line">{{ line }}</p>
					</div>
				</div>
			</div>
		</div>
	</section>
</template>

<script setup>
	import '../style/global.scss';

	const skills = [
		{
			id: 2,
			tipo: 'Desenvolvimento',
			sigla: 'dev',
			descricao: ['Javascript | Typescript', 'Vue.js | React', 'C# .NET', 'Python', 'Java'],
		},
		{
			id: 1,
			tipo: 'Ferramentas',
			sigla: 'ferramentas',
			descricao: ['Banco de Dados SQL Server', 'Microsoft Power BI, Excel, Power Automate e Sharepoint'],
		},
	];
</script>

<style lang="scss" scoped></style>
