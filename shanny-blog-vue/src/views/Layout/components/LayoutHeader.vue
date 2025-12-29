<script setup>
import LanguageToggle from '@/components/header/LanguageToggle.vue'
import ThemeToggle from '@/components/header/ThemeToggle.vue'
import DrawerComponent from '@/components/common/DrawerComponent.vue'
import SearchComponent from '@/components/header/SearchComponent.vue'
import { translate } from '@/config/translate'
import { useLanguageStore } from '@/stores/modules/language'
import { useScrollStore } from '@/stores/modules/scroll'
import { menuItems } from '@/config/menuItem'
const languageStore = useLanguageStore()
const scrollStore = useScrollStore()
</script>
<template>
  <div
    class="z-99 fixed top-0 w-full flex justify-center items-center pt-3 pb-3 pl-3 pr-3 shadow-md bg-base-200 md:pt-5 md:pb-5 md:transition-all md:duration-300 md:ease-in-out"
    :class="
      scrollStore.isScrolled
        ? 'md:pl-0 md:pr-0'
        : 'md:w-7/10 md:mt-8 md:mb-8 md:rounded-full md:pl-10 md:pr-10'
    "
  >
    <div
      class="flex justify-between items-center w-full"
      :class="scrollStore.isScrolled ? 'md:w-7/10' : ''"
    >
      <div class="flex items-center md:w-1/5">
        <DrawerComponent
          class="block md:hidden"
          id="mobile-drawer"
          :items="menuItems()"
        />
        <h1 class="hidden md:block">
          <img src="@/assets/images/logo.png" class="w-16" />
        </h1>
      </div>
      <!-- bar -->
      <div class="hidden md:flex justify-center items-center w-3/5 font-medium">
        <ul class="flex justify-center items-center w-full">
          <template v-for="(item, index) in menuItems()" :key="index">
            <li
              v-if="!item.children"
              :class="index == 0 ? '' : 'ml-5'"
              class="hover:underline"
            >
              <a :href="item.path">{{ item.title }}</a>
            </li>
            <li v-else class="ml-5">
              <div class="dropdown dropdown-hover">
                <div
                  tabindex="0"
                  role="button"
                  class="flex items-center hover:underline"
                >
                  <span>{{ item.title }}</span>
                  <font-awesome-icon
                    icon="fa-solid fa-chevron-down"
                    class="text-xxs ml-1"
                  />
                </div>
                <ul
                  tabindex="0"
                  class="dropdown-content menu bg-base-200 rounded-box z-1 w-52 p-2 shadow-sm"
                >
                  <li v-for="(child, iChild) in item.children" :key="iChild">
                    <a
                      :href="child.path"
                      class="hover-bg-primary active-bg-primary"
                    >
                      {{ child.title }}
                    </a>
                  </li>
                </ul>
              </div>
            </li>
          </template>
        </ul>
      </div>
      <div class="flex justify-end items-center md:w-1/5">
        <!-- search -->
        <div>
          <SearchComponent id="header-search" />
        </div>
        <!-- change theme -->
        <div class="ml-5">
          <ThemeToggle />
        </div>
        <!-- change language -->
        <div class="ml-5">
          <LanguageToggle />
        </div>
      </div>
    </div>
  </div>
</template>
<style lang="scss" scoped></style>
