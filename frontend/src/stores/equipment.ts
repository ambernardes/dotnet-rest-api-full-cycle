import { ref } from 'vue'
import { defineStore } from 'pinia'

export const useEquipmentStore = defineStore('equipment', () => {
  const equipment = ref(undefined as Equipment | undefined)
  return { equipment }
})

export type Equipment = {
  id: number
  name: string
  category: string
  status: string
  location: string
  lastMaintenanceDate: Date
  rentalRate: number
}
