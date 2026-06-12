<script setup>
defineProps({
  id: {
    type: String,
  },
})
</script>
<template>
  <label :for="id" class="btn btn-ghost">
    <font-awesome-icon icon="fa-solid fa-magnifying-glass" />
  </label>
  <input type="checkbox" :id="id" class="modal-toggle" />
  <div class="modal" role="dialog">
    <div class="modal-box search-modal">
      <label
        :for="id"
        class="btn btn-sm btn-circle btn-ghost absolute top-50 right-5 md:top-80"
      >
        <span class="text-xl text-secondary">✕</span>
      </label>
      <h1 class="font-bold text-xl md:text-3xl text-center text-secondary">
        Type to start your search
      </h1>
      <label class="input input-xl input-primary p-1 w-full mt-10">
        <input
          type="text"
          placeholder="input your search"
          class="grow outline-none border-none focus:outline-none text-base pl-3"
        />
        <button class="btn btn-primary">Search</button>
      </label>
    </div>
  </div>
</template>
<style lang="scss" scoped>
.modal-box {
  background-color: transparent !important;
  box-shadow: none !important;
}
</style>
