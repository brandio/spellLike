from tkinter import Tk, ttk, Label, Button, Entry, OptionMenu, StringVar, Checkbutton, BooleanVar
from tkcolorpicker import askcolor

class GUI:
    def __init__(self, master):
        self.master = master
        master.title("Spell Component Maker")

        # Create tab control
        tabControl = ttk.Notebook(master)

        # Add tabs
        ink = ttk.Frame(tabControl)
        tabControl.add(ink, text="Ink")
        Ink(ink)

        pattern = ttk.Frame(tabControl)
        tabControl.add(pattern, text="Pattern")
        Pattern(pattern)

        page = ttk.Frame(tabControl)
        tabControl.add(page, text="Page")
        Page(page)

        language = ttk.Frame(tabControl)
        tabControl.add(language, text="Language")
        Language(language)

        tabControl.pack(expand=1, fill="both")
        #self.greet_button = Button(master, text="Greet", command=self.greet)
        #self.greet_button.pack()

        #self.close_button = Button(master, text="Close", command=master.quit)
        #self.close_button.pack()

class SpellComponentTab:
    def __init__(self,master):
        # Name
        self.nameLabel = Label(master, text="Name").grid(column=0,row=1)
        self.nameEntry = Entry(master, bd=5).grid(column=1,row=1)

        # Tool tip
        self.flavorTextLabel = Label(master, text="Flavor Text").grid(column=2,row=1)
        self.flavorTextLabel = Entry(master, bd=5, width=100).grid(column=3,row=1)

        # Cost
        self.costLabel = Label(master, text="Cost").grid(column=0,row=2)
        self.costEntry = Entry(master, bd=5).grid(column=1,row=2)

        # Export
        self.export = Button(master, text="Export", command=self.Export).grid(column=0,row=0)

    def Export(self):
        pass

class Page(SpellComponentTab):
    def __init__(self,master):
        SpellComponentTab.__init__(self,master)

        # Cooldown
        self.coolDownLabel = Label(master, text="CoolDown").grid(column=0,row=3)
        self.coolDownEntry = Entry(master, bd=5).grid(column=1,row=3)

        # Damage
        self.chaosLabel = Label(master, text="Chaos").grid(column=0,row=4)
        self.chaosEntry = Entry(master, bd=5).grid(column=1,row=4)


class Language(SpellComponentTab):
    def __init__(self,master):
        SpellComponentTab.__init__(self,master)

        # Layers
        self.layers = [
            ("Background", BooleanVar()),
            ("Player", BooleanVar()),
            ("Destructable", BooleanVar()),
        ]
        i = 0
        for text, var in self.layers:
            Checkbutton(master, text=text, variable=var).grid(column=i,row=4)
            i = i + 1

        # Projectory String
        self.ProjectoryLabel = Label(master, text="Projectory").grid(column=0,row=3)
        var = StringVar(master)
        var.set("Option 1")  # initial value
        self.ProjectoryHit = OptionMenu(master,var,"Option 1","Option 2").grid(column=1,row=3)

class Pattern(SpellComponentTab):
    def __init__(self,master):
        SpellComponentTab.__init__(self,master)

        # Pattern
        self.PatternLabel = Label(master, text="Pattern").grid(column=0,row=3)
        var = StringVar(master)
        var.set("Option 1")  # initial value
        self.PatternHit = OptionMenu(master,var,"Option 1","Option 2").grid(column=1,row=3)

class Ink(SpellComponentTab):
    def __init__(self,master):
        SpellComponentTab.__init__(self,master)

        # Color Picker
        self.colorPicker = Button(master, text="Color", command=self.askcolor)
        self.colorPicker.grid(column=0,row=8)

        # Move speed
        self.moveLabel = Label(master, text="Move Speed").grid(column=0,row=3)
        self.moveEntry = Entry(master, bd=5).grid(column=1,row=3)

        # Damage mod
        self.damageLabel = Label(master, text="Damage Mod").grid(column=0,row=4)
        self.DamageEntry = Entry(master, bd=5).grid(column=1,row=4)

        # Priority
        self.priorityLabel = Label(master, text="Priority").grid(column=0,row=5)
        self.priorityEntry = Entry(master, bd=5).grid(column=1,row=5)

        # On hit effect
        self.onHitEffectLabel = Label(master, text="On Hit Effect").grid(column=0,row=6)
        var = StringVar(master)
        var.set("Option 1")  # initial value
        self.onHit = OptionMenu(master,var,"Option 1","Option 2").grid(column=1,row=6)

        # On collision effect
        self.onCollisionEffectLabel = Label(master, text="On Collision Effect").grid(column=0,row=7)
        var2 = StringVar(master)
        var2.set("Option 1")  # initial value
        self.onCollision = OptionMenu(master,var,"Option 1","Option 2").grid(column=1,row=7)

    def askcolor(self):
        self.color = askcolor(color="white")
        self.colorPicker.configure(bg = self.color[1])

root = Tk()
my_gui = GUI(root)
root.mainloop()