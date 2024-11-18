import api from './api'
import type { Equipment } from '@/stores/equipment'

export const getAll = async (): Promise<Equipment[]> => {
  try {
    const response = await api.get('/equipment')
    return response.data
  } catch (error) {
    console.error('Error fetching equipment data:', error)
    return []
  }
}

export async function deleteEquipment(id: number): Promise<void> {
  try {
    await api.delete(`/equipment/${id}`)
    console.log('Equipment deleted successfully')
  } catch (error) {
    console.error('Error deleting equipment:', error)
    throw error
  }
}

export async function addEquipment(item: Equipment): Promise<Equipment> {
  try {
    const response = await api.post('/equipment', item)
    return response.data
  } catch (error) {
    console.error('Error adding equipment:', error)
    throw error
  }
}

export async function updateEquipment(id: number, item: Equipment): Promise<Equipment> {
  try {
    const response = await api.put(`/equipment/${id}`, item)
    return response.data
  } catch (error) {
    console.error('Error updating equipment:', error)
    throw error
  }
}
