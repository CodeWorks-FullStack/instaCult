<template>
  <router-link :to="{ name: 'CultDetails', params: { id: cult.id } }">
    <div class="text-center p-2 border border-danger rounded elevation-5 selectable">
      <img class="rounded-pill" :src="cult.leader.picture" :alt="cult.leader.name">
      <h3>{{ cult.name }}</h3>
      <b v-for="n in cult.memberCount">♟️</b>
      <b v-for="n in cult.calcMemberCount">🦧</b>
    </div>
  </router-link>
</template>


<script setup>
import { AppState } from '../AppState';
import { computed, reactive, onMounted } from 'vue';

const props = defineProps({ cult: { type: Object, required: true } })


</script>


<style lang="scss" scoped>
div {
  word-wrap: break-word;
}
</style>
