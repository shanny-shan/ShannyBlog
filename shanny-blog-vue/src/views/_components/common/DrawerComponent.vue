<script setup>
import { useSiteStore } from '@/stores'
const siteStore = useSiteStore()
defineProps({
  id: {
    type: String,
  },
  items: {
    type: Array,
    default: () => [],
  },
})
</script>
<template>
  <div class="drawer">
    <input
      :id="id"
      type="checkbox"
      class="drawer-toggle"
      v-model="siteStore.drawerOpen"
    />
    <div class="drawer-content">
      <label :for="id" class="btn btn-square btn-ghost">
        <svg
          xmlns="http://www.w3.org/2000/svg"
          class="h-5 w-5"
          fill="none"
          viewBox="0 0 24 24"
          stroke="currentColor"
        >
          <path
            stroke-linecap="round"
            stroke-linejoin="round"
            stroke-width="2"
            d="M4 6h16M4 12h16M4 18h7"
          />
        </svg>
      </label>
    </div>
    <div class="drawer-side">
      <label
        :for="id"
        aria-label="close sidebar"
        class="drawer-overlay"
        @click="
          () => {
            siteStore.closeDrawer
          }
        "
      ></label>
      <ul class="menu bg-base-100 w-70 h-full">
        <template v-for="item in items" :key="item.id">
          <li v-if="!item.children">
            <RouterLink
              :to="item.path"
              class="md:hover-bg-primary"
              :class="siteStore.curHref == item.id ? 'menu-active' : ''"
              @click="siteStore.closeDrawer"
            >
              {{ item.title }}
            </RouterLink>
          </li>
          <li v-else>
            <h2 class="menu-title">{{ item.title }}</h2>
            <ul>
              <li v-for="child in item.children" :key="child.id">
                <RouterLink
                  :to="child.path"
                  class="md:hover-bg-primary"
                  :class="siteStore.curHref == child.id ? 'menu-active' : ''"
                  @click="siteStore.closeDrawer"
                >
                  {{ child.title }}
                </RouterLink>
              </li>
            </ul>
          </li>
        </template>
      </ul>
    </div>
  </div>
</template>

<style lang="scss" scoped></style>
