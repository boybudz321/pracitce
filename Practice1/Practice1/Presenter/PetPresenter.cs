using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Practice1.Model;
using Practice1.View;

namespace Practice1.Presenter
{
    public class PetPresenter
    {
        // Fields
        private IPetView view;
        private IPetRepository repository;
        private BindingSource petsBindingSource;
        private IEnumerable<PetModel> petLists;

        // COnstructor
        public PetPresenter(IPetView view, IPetRepository repository)
        {
            this.petsBindingSource = new BindingSource();
            this.view = view;
            this.repository = repository;
            // Subscribe event handler methods to the view
            this.view.SearchEvent += SearchPet;
            this.view.AddNewEvent += AddNewPet;
            this.view.EditEvent += LoadSelectedPetToEdit;
            this.view.DeleteEvent += DeleteSelectedPet;
            this.view.SaveEvent += SaveEvent;
            this.view.CancelEvent += CancelAction;

            // Set pet binding source
            this.view.SetPetListBindingSource(petsBindingSource);

            // Load pet list view
            LoadAllPetList();

            // Show View
            this.view.Show();
        }

        // Methods
        private void LoadAllPetList()
        {
            petLists = repository.GetAll();
            petsBindingSource.DataSource  = petLists; //Set Data Source

        }
        private void SearchPet(object sender, EventArgs e)
        {
            bool emptyValue = string.IsNullOrWhiteSpace(this.view.SearchValue);
            if(emptyValue == false)
            {
                petLists = repository.GetByValue(this.view.SearchValue);
            }
            else
            {
                petLists = repository.GetAll();
            }
            petsBindingSource.DataSource = petLists;
        }

        private void CancelAction(object sender, EventArgs e)
        {
            CleanViewFields();
        }

        private void SaveEvent(object sender, EventArgs e)
        {
            var model = new PetModel();
            model.Id = Convert.ToInt32(view.PetID);
            model.Name = view.PetName;
            model.Type = view.PetType;
            model.Colour = view.PetColour; 
            try
            {
                new Common.ModelDataValidation().Validate(model);
                if(view.IsEdit)
                {
                    repository.Edit(model);
                    view.Message = "Pet Edited Successfuly";
                }
                else
                {
                    model.Id = 0;
                    repository.Add(model);
                    view.Message = "Pet Added SUccessfuly";
                }
                view.IsSuccessful = true;
                LoadAllPetList();
                CleanViewFields();

            }
            catch(Exception ex)
            {
                view.IsSuccessful = false;
                view.Message = ex.Message;
            }
        }

        private void CleanViewFields()
        {
            view.PetID = "0";
            view.PetName = "";
            view.PetType = "";
            view.PetColour = "";
        }

        private void DeleteSelectedPet(object sender, EventArgs e)
        {
            try
            {
                var pet = (PetModel)petsBindingSource.Current;
                repository.Delete(pet.Id);
                view.IsSuccessful = true;
                view.Message = "Pet deleted successfuly";
                LoadAllPetList();

            }
            catch (Exception ex)
            {
                view.IsSuccessful = false;
                view.Message = "An error occured";
            }
        }

        private void LoadSelectedPetToEdit(object sender, EventArgs e)
        {
            var pet = (PetModel)petsBindingSource.Current;
            view.PetID = pet.Id.ToString();
            view.PetName = pet.Name;
            view.PetType = pet.Type;
            view.PetColour = pet.Colour;
            view.IsEdit = true;
        }

        private void AddNewPet(object sender, EventArgs e)
        {
            view.IsEdit = false;
        }

        
    }
}
