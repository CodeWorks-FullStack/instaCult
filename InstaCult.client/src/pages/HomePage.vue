<template>
  <div class="container-fluid text-primary">
    <section class="row pt-3">
      <div class="col-4" v-for="c in cults">
        <CultCard :cult="c" />
      </div>
    </section>
  </div>
</template>

<script>
import { onMounted, computed } from 'vue';
import Pop from '../utils/Pop.js';
import { cultsService } from '../services/CultsService.js';
import { AppState } from '../AppState.js';

export default {
  setup() {
    onMounted(() => {
      getCults()
    })
    async function getCults() {
      try {
        await cultsService.getCults()
      } catch (error) {
        Pop.error(error, 'Getting Cults')
      }
    }
    return {
      cults: computed(() => AppState.cults)
    }
  }
}
</script>

<style scoped lang="scss">
.home {
  display: grid;
  height: 80vh;
  place-content: center;
  text-align: center;
  user-select: none;

  .home-card {
    width: 50vw;

    >img {
      height: 200px;
      max-width: 200px;
      width: 100%;
      object-fit: contain;
      object-position: center;
    }
  }
}
</style>
