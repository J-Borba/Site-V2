<template>
	<section>
		<div class="row w-75">
			<div class="col-12 text-center mb-4">
				<p class="h2 primary">Última Experiência</p>
			</div>

			<div class="col-lg-3 col-md-12 m-auto text-center">
				<p class="h5">TIPLAN</p>
				<p class="secondary">Desenvolvedor de Software</p>
				<p class="primary">Out/2023 - Atualmente</p>
			</div>

			<div class="col-lg-6 col-md-12 my-4">
				<iframe
					class="rounded-2 w-100 h-100"
					src="https://www.google.com/maps/embed?pb=!1m14!1m8!1m3!1d3468.95989963365!2d-43.18144200464534!3d-22.904709338182126!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x997f5fdc3e9565%3A0xe09773af0e23b78e!2sTIPLAN!5e0!3m2!1spt-BR!2sbr!4v1696688192919!5m2!1spt-BR!2sbr"
					style="border: 0"
					loading="lazy"
					referrerpolicy="no-referrer-when-downgrade">
				</iframe>
			</div>
			<div class="col-lg-3 col-md-12 m-auto text-center">
				<p class="h5 mb-2">Experiências</p>
				<router-link to="experiencia">
					<font-awesome-icon class="secondary" :icon="['fas', 'circle-info']" size="xl" />
				</router-link>
			</div>
		</div>
	</section>
</template>

<script setup>
	import '../../style/global.scss';
</script>

<style lang="scss" scoped></style>
