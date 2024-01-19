<template>
	<MyHeader />
	<transition @before-enter="scrollTop" mode="out-in" appear>
		<router-view />
	</transition>
	<WhatsappBtn />
	<MyFooter />
</template>

<script setup>
	import MyHeader from './0-Global/components/MyHeader.vue';
	import MyFooter from './0-Global/components/MyFooter.vue';
	import WhatsappBtn from './0-Global/components/WhatsappBtn.vue';

	const scrollTop = () => {
		document.getElementById('app').scrollIntoView();
	};
</script>
