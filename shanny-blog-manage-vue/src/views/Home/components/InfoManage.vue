<script setup>
import { onMounted } from 'vue'
import InfoDialog from '@/views/_components/dialog/InfoDialog.vue'
import { useAdminStore, useAccountStore } from '@/stores'

const accountStore = useAccountStore()
const adminStore = useAdminStore()

const editInfo = (item) => {
  adminStore.openDialog('info')
  accountStore.userForm = {
    ...item,
    userDetails: { ...item.userDetails },
  }
  adminStore.isEdit = true
}

onMounted(async () => {
  await accountStore.getAllUsers()
})
</script>
<template>
  <div>
    <!-- <div class="flex justify-end gap-2">
      <button class="btn btn-primary" @click="adminStore.openDialog('info')">
        Add User
      </button>
    </div> -->
    <div class="overflow-x-auto mt-2">
      <table class="table">
        <!-- head -->
        <thead>
          <tr>
            <th>
              <label>
                <input type="checkbox" class="checkbox" />
              </label>
            </th>
            <th>UserId</th>
            <th>nickName</th>
            <th>userName</th>
            <th>birthday</th>
            <th>sex</th>
            <th>mobile</th>
            <th>lastLogin</th>
            <th>other</th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="(item, index) in accountStore.users" :key="index">
            <th>
              <label>
                <input type="checkbox" class="checkbox" />
              </label>
            </th>
            <td>
              <div class="flex items-center gap-3">
                <div class="avatar">
                  <div class="mask mask-squircle h-12 w-12">
                    <img
                      src="@/assets/images/avatar.jpg"
                      alt="Avatar Tailwind CSS Component"
                    />
                  </div>
                </div>
                <div>
                  <div class="font-bold">{{ item.userId }}</div>
                  <div class="text-sm opacity-50">{{ item.status }}</div>
                </div>
              </div>
            </td>
            <td>{{ item.userDetails?.nickname }}</td>
            <td>{{ item.userDetails?.username }}</td>
            <td>{{ item.userDetails?.birthday }}</td>
            <td>{{ item.userDetails?.sex }}</td>
            <td>{{ item.mobile }}</td>
            <td>{{ item.lastLoginTime.substring(0, 10) }}</td>
            <th>
              <button class="btn btn-ghost btn-xs" @click="editInfo(item)">
                Edit
              </button>
            </th>
          </tr>
        </tbody>
      </table>
    </div>
  </div>
  <InfoDialog />
</template>
<style lang="scss" scoped></style>
