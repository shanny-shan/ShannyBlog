<script setup>
defineProps({
  index: {
    type: Number,
  },
  item: {
    type: Object,
    default: () => {},
  },
  footer: {
    type: Boolean,
    default: true,
  },
})
</script>
<template>
  <div class="card bg-base-200 shadow-sm flex w-full">
    <figure class="w-full">
      <img src="@/assets/images/avatar.jpg" alt="Movie" />
    </figure>
    <div class="card-body w-full p-3 flex flex-col justify-between">
      <div>
        <h2 class="card-title text-2xl">{{ item.title }}</h2>
        <p class="text-base mt-3">{{ item.content }}</p>
      </div>
      <div class="flex justify-between items-center" v-if="footer">
        <div class="flex justify-end items-center">
          <img src="@/assets/images/avatar.jpg" class="w-5 rounded-full" />
          <span class="ml-1 text-xs">{{ item.author }}</span>
        </div>
        <div>
          <font-awesome-icon
            icon="fa-regular fa-calendar"
            class="text-primary"
          />
          <span class="ml-1 text-xs font-light text-neutral">{{
            item.date
          }}</span>
        </div>
      </div>
    </div>
  </div>
</template>
<style lang="scss" scoped></style>
